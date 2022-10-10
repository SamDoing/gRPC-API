using Grpc.Core;
using gRPC_API;
using gRPC_API.Data.Repositorys;
using gRPC_API.Model;
using gRPC_API.Models.Utils;

namespace gRPC_API.Services
{
    public class ProductService : gRPC_API.ProductService.ProductServiceBase
    {
        private readonly ILogger<ProductService> _logger;
        private readonly ProductRepository _productRepository;
        public ProductService(ILogger<ProductService> logger, ProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public override async Task<ProductReply> GetProduct(GetProductRequest request, ServerCallContext context)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id);
            return ConverterDto.Converter<ProductReply, Product?>(product);
        }

        public override async Task<ProductReply> CreateProduct(CreateProductRequest request, ServerCallContext context)
        {

            var product = await _productRepository.CreateProductAsync(ConverterDto.Converter<Product, CreateProductRequest>(request));

            return ConverterDto.Converter<ProductReply, Product>(product);
        }

        public override async Task<pVoid> DeleteProduct(GetProductRequest request, ServerCallContext context)
        {
            await _productRepository.DeleteProductBydIdAsync(request.Id);
            return new();
        }

        public override async Task<ProductReply> EditProduct(EditProductRequest request, ServerCallContext context)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id);
            
            ConverterDto.ConvertInPlace(request.Product, product);

            await _productRepository.UpdateProductAsync(product);

            return ConverterDto.Converter<ProductReply, Product>(product);
        }
    }
}