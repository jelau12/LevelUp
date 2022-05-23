using LevelUp.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LevelUp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //fields
        private LevelUpDbContext _context;

        //constructer
        public ProductsController(LevelUpDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all products from database
        /// </summary>
        /// <returns>All products from database in json</returns>
        [HttpGet("GetAllProducts")]
        #region GetAllProducts
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        } 
        #endregion

        /// <summary>
        /// Gets product with <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("GetProductById/{id:int}")]
        #region GetProductById
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

            if (product == null)
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
            //check if any model errors have been added to ModelState
            if (ModelState.IsValid)
            {
                //add to protduct to db
                _context.Add(product);
                await _context.SaveChangesAsync();
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
            var Product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            _context.Products.Remove(Product);
            await _context.SaveChangesAsync();

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
            //check for modelstate errors
            if (ModelState.IsValid)
            {
                try
                {
                    //try to update in db
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //return 404 if product dosent exsist
                    if (!ProductExists(product.Id))
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

        //Checks if the product exists in database
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}