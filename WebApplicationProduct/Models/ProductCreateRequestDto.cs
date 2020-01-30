using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationProduct.Models
{
    public class ProductCreateRequestDto
    {
        [Required(ErrorMessage = "Prosze podać nazwę")]
        public string Name { get; set; }
        [RegularExpression("(^[1-9]+[0-9]*)", ErrorMessage = "Enter a valid decimal number")]
        [Required(ErrorMessage = "Prosze podać cenę")]
        public decimal Price { get; set; }

    }
}
