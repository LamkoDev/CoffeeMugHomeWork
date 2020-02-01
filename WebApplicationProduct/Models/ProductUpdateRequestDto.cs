using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationProduct.Models
{
    public class ProductUpdateRequestDto
    {
        [Required(ErrorMessage = "Prosze podać id")]
        [RegularExpression(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$", ErrorMessage = "Enter a valid guid")]
        
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Prosze podać nazwę")]
        [StringLength(100, MinimumLength = 3)]
        public string NewName { get; set; }

        [RegularExpression(@"^[0-9]*\,?[0-9]+$", ErrorMessage = "Enter a valid price")]
        [Required(ErrorMessage = "Prosze podać cenę")]
        public decimal NewPrice { get; set; }
    }
}
