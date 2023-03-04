﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vegefood.Models;
using Vegefood.Models.Interfaces;

namespace Vegefood.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class InventoryController : Controller
    {
        private readonly IInventory _context;
        public InventoryController(IInventory inventory)
        {
            _context = inventory;
        }
        // GET: Inventory
        /// <summary>
        /// Default HTTP GET route for /Inventory to display all of the product data from the connencted database
        /// </summary>
        /// <returns>Index.cshtml with all of product data from the connected database</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetAllInventoriesAsync());
        }

        // GET: Inventory/Detail/5
        /// <summary>
        /// HTTP GET route for Inventory/Edit to get a product data based on the product Id from the connencted database
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Edit.cshtml with product data that matches with the id from the conntected database</returns>
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            Product product = await _context.GetInventoryByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        /// <summary>
        /// HTTP GET route for Inventory/Create
        /// </summary>
        /// <returns>Empty Create.cshtml</returns>
        public IActionResult Create()
        {
            return View();
        }

        // Source: https://www.c-sharpcorner.com/article/upload-files-in-azure-blob-storage-using-asp-net-core/
        // POST: Inventory/Create
        /// <summary>
        /// HTTP POST route for Inventory/Create to create a new product data by saving a Product object into the connected database
        /// </summary>
        /// <param name="product">New product information</param>
        /// <returns>Index.cshtml with the updated inventory list from the the conntected database</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            bool isValid = true;
            if (product.Quantity < 0)
            {
                ModelState.AddModelError(nameof(product.Quantity), "The number must be positive.");
                isValid = false;
            }
            if (product.Price < 0)
            {
                ModelState.AddModelError(nameof(product.Price), "The number must be positive.");
                isValid = false;
            }
            if (product.Name == null || product.Description == null)
            {
                isValid = false;
            }
            if (isValid)
            {
                ModelState.Clear();
            }
            if (ModelState.IsValid)
            {
                await _context.CreateInventoryAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Inventory/Edit/5
        /// <summary>
        /// HTTP GET route for Inventory/Edit to get a product data based on the product Id from the connected database
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Edit.cshtml with a product infomration based on the product Id from the connected database</returns>
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var product = await _context.GetInventoryByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST: Inventory/Edit/5
        /// <summary>
        /// HTTP POST route for Inventory/Edit/ to edit the product data from the connected database
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="product">Product data based on the Id</param>
        /// <returns>Index.cshtml with the updated inventory list from the the conntected database</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ID)
            {
                return NotFound();
            }
            bool isValid = true;
            if (product.Quantity < 0)
            {
                ModelState.AddModelError(nameof(product.Quantity), "The number must be positive.");
                isValid = false;
            }
            if (product.Price < 0)
            {
                ModelState.AddModelError(nameof(product.Price), "The number must be positive.");
                isValid = false;
            }
            if (product.Name == null || product.Description == null)
            {
                isValid = false;
            }
            if (isValid)
            {
                ModelState.Clear();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.UpdateInventoryAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExists(product.ID))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {

                return View(product);
            }
        }

        // GET: Inventory/Delete/5
        /// <summary>
        /// HTTP GET route for Inventory/Delete/ to get a product data based on the product Id to be deleted from the connected database
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Delete.cshtml with a product data based on the product Id</returns>
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var product = await _context.GetInventoryByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Inventory/Delete/5
        /// <summary>
        /// HTTP POST route for Inventory/Delete/ to delete a product data based on the producrt Id from the connected database
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Index.cshtml with the updated inventory list from the the conntected database</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var product = await _context.GetInventoryByIdAsync(id);

            await _context.RemoveInventoryAsync(id);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// An action that checks if a product data exists based on the product Id in the connected database
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Boolean value which confirms if a produuct data based on the product Id exists</returns>
        private async Task<bool> ProductExists(int id)
        {
            Product product = await _context.GetInventoryByIdAsync(id);
            if (product != null)
            {
                return true;
            }
            return false;
        }
    }
}
