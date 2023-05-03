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
using MagicVilla_API.Repository.IRepository;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroVillaController : ControllerBase
    {
        //Log de las operaciones en el servidor
        private readonly ILogger<NumeroVillaController> _logger;
        /*//Inyeccion de dependencia
        private readonly ApplicationDbContext _db;*/
        private readonly IVillaRepository _villaRepo;
        private readonly INumeroVillaRepository _NumerovillaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepository villaRepo,IMapper mapper, INumeroVillaRepository numerovillaRepo)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _NumerovillaRepo = numerovillaRepo;
            _mapper = mapper;
            _response = new ();
        }

        [HttpGet]
        public async Task <ActionResult<APIResponse>> GetNumeroVillas()
        {
            try
            {
                _logger.LogInformation("Obtener numeros  villas");

                IEnumerable<NumeroVilla> numerovillalist = await _NumerovillaRepo.GetAll();
                _response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(numerovillalist);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
           


        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Numero Villa con id" + id);
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                var numerovilla = await _NumerovillaRepo.Obtener(v => v.VillaNo == id);

                if (numerovilla == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Resultado= _mapper.Map<NumeroVillaDto>(numerovilla);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess= false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateNumeroVilla([FromBody] NumeroVillaCreateDto CreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //if (VillaStore.villaList.FirstOrDefault(v => v.Name.ToLower() == villaDto.Name.ToLower()) != null) {
                if (await _NumerovillaRepo.Obtener(v => v.VillaNo == CreateDto.VillNo) != null)
                {
                    ModelState.AddModelError("Existing Name", "El numero de La villa con ese nombre ya existe!");
                    return BadRequest(ModelState);
                }
                if(await _villaRepo.Obtener(v=>v.Id== CreateDto.VillaId) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de la villa no existe!");
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
                NumeroVilla modelo = _mapper.Map<NumeroVilla>(CreateDto);
                modelo.UpdateDate = DateTime.Now;
                modelo.CreateDate = DateTime.Now;
                await _NumerovillaRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.StatusCode = HttpStatusCode.Created;
                /*await _db.Villas.AddAsync(modelo);
                await _db.SaveChangesAsync();*/
                return CreatedAtRoute("GetVilla", new { id = modelo.VillaNo }, _response);
                //return Ok(villaDto);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult<APIResponse>> DeleteVilla(int id)
        public async Task<IActionResult> DeleteNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                var numerovilla = await _NumerovillaRepo.Obtener(v => v.VillaNo == id);
                if (numerovilla == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode=HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                //VillaStore.villaList.Remove(villa);
                await _NumerovillaRepo.Remover(numerovilla);

                _response.StatusCode= HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return BadRequest(_response);
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatenumeroVilla(int id, [FromBody] NumeroVillaUpdateDto UpdateDto)
        {
            if (UpdateDto == null || id != UpdateDto.VillNo)
            {
                _response.IsSuccess = false;
                _response.StatusCode=HttpStatusCode.BadRequest;
                return BadRequest(_response);
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
            if (await _villaRepo.Obtener(v => v.Id == UpdateDto.VillaId) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id de la villa no existe!");
                return BadRequest(ModelState);
            }
            NumeroVilla modelo = _mapper.Map<NumeroVilla>(UpdateDto);
            /*_db.Villas.Update(modelo);
            await _db.SaveChangesAsync();*/
            await _NumerovillaRepo.Actualizar(modelo);
            _response.StatusCode=HttpStatusCode.NoContent;
            return Ok(_response);
        }
    }
}
