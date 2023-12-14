using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NiceToes.ProductAPI.Data;
using NiceToes.ProductAPI.Models;
using NiceToes.ProductAPI.Models.Dto;
using NiceToes.ProductAPI.Extensions;

namespace NiceToes.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ResponseDto _response;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductAPIController> _logger;

        public ProductAPIController(AppDbContext db, IMapper mapper, ILogger<ProductAPIController> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
            _response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            _logger.LogInformation("Started getting all the products");

            try
            {
                IEnumerable<Product> objList = _db.Products.ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);

                _logger.LogInformation("Got all the products");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

                _logger.LogInformation("Did not get all the products");
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Product obj = _db.Products.First(u=>u.ProductId==id);
                _response.Result = _mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

       [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post(ProductDto ProductDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(ProductDto);
                _db.Products.Add(product);
                _db.SaveChanges();

                if (ProductDto.Image != null)
                {
                   
                    string fileName = product.ProductId + Path.GetExtension(ProductDto.Image.FileName);
                    string filePath = @"wwwroot\Images\" + fileName;

                    //I have added the if condition to remove the any image with same name if that exist in the folder by any change
                        var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                        FileInfo file = new FileInfo(directoryLocation);
                        if (file.Exists)
                        {
                            file.Delete();
                        }

                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        ProductDto.Image.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    product.ImageUrl = baseUrl+ "/Images/" + fileName;
                    product.ImageLocalPath = filePath;
                }
                else
                {
                    product.ImageUrl = "https://placehold.co/600x400";
                }
                _db.Products.Update(product);
                _db.SaveChanges();
                _response.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put(ProductDto ProductDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(ProductDto);

                if (ProductDto.Image != null)
                {
                    if (!string.IsNullOrEmpty(product.ImageLocalPath))
                    {
                        var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), product.ImageLocalPath);
                        FileInfo file = new FileInfo(oldFilePathDirectory);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }

                    string fileName = product.ProductId + Path.GetExtension(ProductDto.Image.FileName);
                    string filePath = @"wwwroot\Images\" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        ProductDto.Image.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    product.ImageUrl = baseUrl + "/Images/" + fileName;
                    product.ImageLocalPath = filePath;
                }


                _db.Products.Update(product);
                _db.SaveChanges();

                _response.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Product obj = _db.Products.First(u=>u.ProductId==id);
                if (!string.IsNullOrEmpty(obj.ImageLocalPath))
                {
                    var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), obj.ImageLocalPath);
                    FileInfo file = new FileInfo(oldFilePathDirectory);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                _db.Products.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
