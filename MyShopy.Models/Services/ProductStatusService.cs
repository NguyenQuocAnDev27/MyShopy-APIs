using MyShopy.Models.Interfaces;
using MyShopy.Models.Common;
using MyShopy.Models.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MyShopy.Models.Services
{
    public class ProductStatusService : IProductStatusService
    {
        private readonly IDbService _dbService;
        private readonly ILogger<ProductStatusService> _logger;

        public ProductStatusService(IDbService dbService, ILogger<ProductStatusService> logger)
        {
            _dbService = dbService;
            _logger = logger;
        }

        private ProductStatus MapDataRowToProductStatus(DataRow row)
        {
            return new ProductStatus
            {
                StatusId = Global.GetIDinDT(row.Table, row.Table.Rows.IndexOf(row), "status_id"),
                Usid = Global.GetinDT_String(row.Table, row.Table.Rows.IndexOf(row), "usid"),
                Description = Global.GetinDT_String(row.Table, row.Table.Rows.IndexOf(row), "description"),
                InStock = Convert.ToBoolean(row["in_stock"]),
                Buyable = Convert.ToBoolean(row["buyable"]),
                Shippable = Convert.ToBoolean(row["shippable"]),
                Active = Convert.ToBoolean(row["active"]),
                CreatedAt = Convert.ToDateTime(row["created_at"]),
                UpdatedAt = Convert.ToDateTime(row["updated_at"])
            };
        }

        public async Task<IEnumerable<ProductStatus>> GetProductStatusesAsync()
        {
            try
            {
                _logger.LogInformation("Getting all product statuses");
                _dbService.OpenConnection();
                var dt = _dbService.ExecuteReaderCommand("SELECT * FROM ProductStatuses", "ProductStatuses");
                var productStatuses = new List<ProductStatus>();

                foreach (DataRow row in dt.Rows)
                {
                    productStatuses.Add(MapDataRowToProductStatus(row));
                }

                _dbService.CloseConnection();
                _logger.LogInformation("Successfully retrieved product statuses");
                return productStatuses;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving product statuses: {ex.Message}");
                throw;
            }
        }

        public async Task<ProductStatus> GetProductStatusAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Getting product status with ID {id}");
                _dbService.OpenConnection();
                var dt = _dbService.ExecuteReaderCommand($"SELECT * FROM ProductStatuses WHERE status_id = {id}", "ProductStatus");

                if (Global.DTCount(dt) == 0)
                {
                    _dbService.CloseConnection();
                    _logger.LogWarning($"Product status with ID {id} not found");
                    return null;
                }

                var productStatus = MapDataRowToProductStatus(dt.Rows[0]);
                _dbService.CloseConnection();
                _logger.LogInformation($"Successfully retrieved product status with ID {id}");
                return productStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving product status with ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateProductStatusAsync(ProductStatus productStatus)
        {
            try
            {
                _logger.LogInformation($"Updating product status with ID {productStatus.StatusId}");
                _dbService.OpenConnection();
                var command = _dbService.CreateCommand(
                    "UPDATE ProductStatuses SET usid = @Usid, description = @Description, in_stock = @InStock, buyable = @Buyable, shippable = @Shippable, active = @Active, updated_at = @UpdatedAt WHERE status_id = @StatusId"
                );

                _dbService.AddParameterWithValue(command, "@StatusId", productStatus.StatusId);
                _dbService.AddParameterWithValue(command, "@Usid", productStatus.Usid);
                _dbService.AddParameterWithValue(command, "@Description", productStatus.Description);
                _dbService.AddParameterWithValue(command, "@InStock", productStatus.InStock);
                _dbService.AddParameterWithValue(command, "@Buyable", productStatus.Buyable);
                _dbService.AddParameterWithValue(command, "@Shippable", productStatus.Shippable);
                _dbService.AddParameterWithValue(command, "@Active", productStatus.Active);
                _dbService.AddParameterWithValue(command, "@UpdatedAt", DateTime.UtcNow);

                _dbService.ExecuteNonQueryCommand(command);
                _dbService.CloseConnection();
                _logger.LogInformation($"Successfully updated product status with ID {productStatus.StatusId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating product status with ID {productStatus.StatusId}: {ex.Message}");
                throw;
            }
        }

        public async Task CreateProductStatusAsync(ProductStatus productStatus)
        {
            try
            {
                _logger.LogInformation("Creating new product status");
                _dbService.OpenConnection();
                var command = _dbService.CreateCommand(
                    "INSERT INTO ProductStatuses (usid, description, in_stock, buyable, shippable, active, created_at, updated_at) VALUES (@Usid, @Description, @InStock, @Buyable, @Shippable, @Active, @CreatedAt, @UpdatedAt)"
                );

                _dbService.AddParameterWithValue(command, "@Usid", productStatus.Usid);
                _dbService.AddParameterWithValue(command, "@Description", productStatus.Description);
                _dbService.AddParameterWithValue(command, "@InStock", productStatus.InStock);
                _dbService.AddParameterWithValue(command, "@Buyable", productStatus.Buyable);
                _dbService.AddParameterWithValue(command, "@Shippable", productStatus.Shippable);
                _dbService.AddParameterWithValue(command, "@Active", productStatus.Active);
                _dbService.AddParameterWithValue(command, "@CreatedAt", DateTime.UtcNow);
                _dbService.AddParameterWithValue(command, "@UpdatedAt", DateTime.UtcNow);

                _dbService.ExecuteNonQueryCommand(command);
                _dbService.CloseConnection();
                _logger.LogInformation("Successfully created new product status");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating new product status: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteProductStatusAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting product status with ID {id}");
                _dbService.OpenConnection();
                var command = _dbService.CreateCommand("DELETE FROM ProductStatuses WHERE status_id = @StatusId");
                _dbService.AddParameterWithValue(command, "@StatusId", id);
                _dbService.ExecuteNonQueryCommand(command);
                _dbService.CloseConnection();
                _logger.LogInformation($"Successfully deleted product status with ID {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting product status with ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
