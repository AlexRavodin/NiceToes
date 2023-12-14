using NiceToes.ShoppingCartAPI.Models.Dto;

namespace NiceToes.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
