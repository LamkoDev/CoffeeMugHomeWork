using System.ComponentModel.DataAnnotations;

namespace WebApplicationProduct.Models

{
    public class GuestResponse
    {
        [Required(ErrorMessage = "Prosze podac imie i nazwisko")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Prosze podac e-mail")]
        [RegularExpression(".+@.+", ErrorMessage = "Zły adres e-mail")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Prosze podac numer tel")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Okreś czy wezmiesz udział")]
        public bool? WillAttend { get; set; }

        public bool emailSended { get; set; }
    }
}
