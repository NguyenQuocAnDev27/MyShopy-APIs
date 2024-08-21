using MyShopy.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShopy.Models.Interfaces
{
    public interface IProductStatusService
    {
        Task<IEnumerable<ProductStatus>> GetProductStatusesAsync();
        Task<ProductStatus> GetProductStatusAsync(int id);
        Task UpdateProductStatusAsync(ProductStatus productStatus);
        Task CreateProductStatusAsync(ProductStatus productStatus);
        Task DeleteProductStatusAsync(int id);
    }
}
