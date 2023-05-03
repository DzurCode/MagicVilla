using MagicVilla_API.Models;

namespace MagicVilla_API.Repository.IRepository
{
    public interface IVillaRepository:IRepository<cVilla>
    {
        Task<cVilla> Actualizar(cVilla entidad);
    }
}
