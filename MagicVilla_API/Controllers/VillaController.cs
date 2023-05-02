using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Datos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        //Log de las operaciones en el servidor
        private readonly ILogger<VillaController> _logger;
        //Inyeccion de dependencia
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db,IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task <ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Obtener las villas");

            IEnumerable<cVilla> villalist = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villalist));
        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer Villa con id" + id);
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDto>(villa));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDto CreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //if (VillaStore.villaList.FirstOrDefault(v => v.Name.ToLower() == villaDto.Name.ToLower()) != null) {
            if (await _db.Villas.FirstOrDefaultAsync(v => v.Name.ToLower() == CreateDto.Name.ToLower()) != null)
            { 
                ModelState.AddModelError("Existing Name", "La villa con ese nombre ya existe!");
                return BadRequest(ModelState);
            }
            if (CreateDto == null)
            {
                return BadRequest(CreateDto);
            }
            /*villaDto.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDto);*/
            /*cVilla modelo = new()
            {
                Name = villaDto.Name,
                Description = villaDto.Description,
                ImageUrl = villaDto.ImageUrl,
                Ocupantes = villaDto.Ocupantes,
                Cost = villaDto.Cost,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad=villaDto.Amenidad,
            };Cambiado por Mapping*/
            cVilla modelo=_mapper.Map<cVilla>(CreateDto);
            await _db.Villas.AddAsync(modelo);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetVilla", new { id = modelo.Id }, modelo);
            //return Ok(villaDto);
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            //VillaStore.villaList.Remove(villa);
            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto UpdateDto)
        {
            if (UpdateDto == null || id != UpdateDto.Id)
            {
                return BadRequest();

            }
            /*var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            villa.Name = villaDto.Name;
            villa.Ocupantes = villaDto.Ocupantes;
            villa.MetrosCuadrados = villaDto.MetrosCuadrados;*/
            /*cVilla modelo = new()
            {
                Id = UpdateDto.Id,
                Name = UpdateDto.Name,
                Description = UpdateDto.Description,
                ImageUrl = UpdateDto.ImageUrl,
                Ocupantes = UpdateDto.Ocupantes,
                Cost = UpdateDto.Cost,
                MetrosCuadrados = UpdateDto.MetrosCuadrados,
                Amenidad = UpdateDto.Amenidad,
            };change by mapping*/
            cVilla modelo = _mapper.Map<cVilla>(UpdateDto);
            _db.Villas.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto>patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();

            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            /*VillaUpdateDto villaDto = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Description = villa.Description,
                ImageUrl = villa.ImageUrl,
                Ocupantes = villa.Ocupantes,
                Cost = villa.Cost,
                MetrosCuadrados = villa.MetrosCuadrados,
                Amenidad = villa.Amenidad,
            };*/

            VillaUpdateDto villaDto=_mapper.Map<VillaUpdateDto>(villa);


            if(villa==null) 
            {
                return BadRequest();
            
            }
            patchDto.ApplyTo(villaDto,ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            /*cVilla modelo = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Description = villaDto.Description,
                ImageUrl = villaDto.ImageUrl,
                Ocupantes = villaDto.Ocupantes,
                Cost = villaDto.Cost,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad,
            };*/

            cVilla modelo = _mapper.Map<cVilla>(villaDto);


            _db.Villas.Update(modelo);
            await _db.SaveChangesAsync();

            return NoContent();
        }

    }
}
