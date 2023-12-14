using AutoMapper;
using NiceToes.ProductAPI.Models;
using NiceToes.ProductAPI.Models.Dto;

namespace NiceToes.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
