﻿namespace Vegefood.Models.Interfaces
{
    public interface IInventory
    {
        // Create
        Task CreateInventoryAsync(Product product);

        // Read - GetAll
        Task<IList<Product>> GetAllInventoriesAsync();

        // Read - GetIsFeatured
        Task<IList<Product>> GetFeaturedInventoriesAsync();

        // Read - GetByID
        Task<Product> GetInventoryByIdAsync(int id);

        // Update
        Task UpdateInventoryAsync(Product product);

        // Delete
        Task RemoveInventoryAsync(int id);
    }
}
