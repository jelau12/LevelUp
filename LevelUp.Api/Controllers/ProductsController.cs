using LevelUp.Api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private LevelUpDbContext _context;

        //constructer
        public ProductsController(LevelUpDbContext context)
        {
            _context = context;
        }


    }
}
