using Business.Dtos;

namespace Business.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(ProductRegistrationDto product);
        Task<IEnumerable<ProductDto>> ReadAllProductsAsync();
        Task<ProductDto> ReadOneProductAsync(string id);
        Task<ProductDto> UpdateProductAsync(ProductDto product);
        Task<bool> DeleteProductAsync(ProductDto product);
    }
}