using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LevelUp.Api.Data
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Price { get; set; }
        public int quantity { get; set; }
        public string PicturePath { get; set; }
    }
}
