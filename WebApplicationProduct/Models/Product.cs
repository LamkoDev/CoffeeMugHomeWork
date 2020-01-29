using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationProduct.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Prosze podać nazwę")]
        public string Name { get; set; }

        [RegularExpression("(^[1-9]+[0-9]*)", ErrorMessage = "Enter a valid price")]
        [Required(ErrorMessage = "Prosze podać cenę")]
        public decimal Price { get; set; }

        public Product(string Name, decimal Price)
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Price = Price;
        }
        public Guid GetId()
        {
            return Id;
        }
    }
}
