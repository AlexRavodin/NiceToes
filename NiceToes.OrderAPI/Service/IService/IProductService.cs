
using NiceToes.OrderAPI.Models.Dto;

namespace NiceToes.OrderAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
