﻿using MagicVilla_API.Models.Dto;

namespace MagicVilla_API.Datos
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto { Id = 1, Name="Vista a la piscina",Ocupantes=2,MetrosCuadrados=100}
            , new VillaDto {Id = 2, Name="Vista a la playa",Ocupantes=4,MetrosCuadrados=80}   
        };
    }
}
