using AutoMapper;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;

namespace MagicVilla_API
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<cVilla, VillaDto>();
            CreateMap<VillaDto, cVilla>();

            CreateMap<cVilla,VillaCreateDto>().ReverseMap();
            CreateMap<cVilla, VillaUpdateDto>().ReverseMap();

            CreateMap<NumeroVilla, NumeroVillaDto>().ReverseMap();
            CreateMap<NumeroVilla, NumeroVillaCreateDto>().ReverseMap();
            CreateMap<NumeroVilla, NumeroVillaUpdateDto>().ReverseMap();
        }
    }
}
