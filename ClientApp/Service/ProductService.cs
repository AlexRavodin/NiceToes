using NiceToes.Web.Models;
using NiceToes.Web.Service.IService;
using NiceToes.Web.Utility;

namespace NiceToes.Web.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateProductsAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.POST,
                Data=productDto,
                Url = Sd.ProductAPIBase + "/api/product" ,
                ContentType = Sd.ContentType.MultipartFormData
            });
        }

        public async Task<ResponseDto?> DeleteProductsAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.DELETE,
                Url = Sd.ProductAPIBase + "/api/product/" + id
            }); 
        }

        public async Task<ResponseDto?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.GET,
                Url = Sd.ProductAPIBase + "/api/product"
            });
        }

      

        public async Task<ResponseDto?> GetProductByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.GET,
                Url = Sd.ProductAPIBase + "/api/product/" + id
            });
        }

        public async Task<ResponseDto?> UpdateProductsAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.PUT,
                Data = productDto,
                Url = Sd.ProductAPIBase + "/api/product",
                ContentType = Sd.ContentType.MultipartFormData
            });
        }
    }
}
