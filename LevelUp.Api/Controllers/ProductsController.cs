using LevelUp.Entities.Models;
using LevelUp.DataAccess;
using LevelUp.Entities.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LevelUp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //fields
        private readonly ProductRepository _repository;

        //constructer
        public ProductsController(ProductRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all products from database
        /// </summary>
        /// <returns>All products from database in json</returns>
        [HttpGet("GetAllProducts")]
        #region GetAllProducts
        public async Task<IActionResult> GetAllProducts()
        {
            //query
            IEnumerable<Product> products = await _repository.GetAllAsync();

            return Ok(products);
        } 
        #endregion

        /// <summary>
        /// Gets product with <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("GetProductById/{id:int}")]
        #region GetProductById
        public async Task<IActionResult> GetProductById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _repository.GetByIdAsync(id);

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        } 
        #endregion

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        #region Create
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Quantity,PicturePath")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _repository.CreateAsync(product);
            }
            return Ok(product);
        } 
        #endregion

        /// <summary>
        /// Delete product from database with <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        // POST: Movies/Delete/5
        [HttpDelete("Delete/{id:int}")]
        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = await _repository.GetByIdAsync(id);

            await _repository.DeleteAsync(product);

            return Ok();
        } 
        #endregion

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("Edit")]
        #region Edit
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Product product)
        {
            if(id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //try to update in db
                    await _repository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExistsAsync(product.Id))
                    {
                        return NotFound();
                    }
                    //else throw exception
                    else
                    {
                        throw;
                    }
                }
            }
            return Ok(product);
        } 
        #endregion

        
        private async Task<bool> ProductExistsAsync(int id)
        {
            Product result = await _repository.GetByIdAsync(id);

            return (result != null);
        }
    }
}