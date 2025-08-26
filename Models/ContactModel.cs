using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContactsApplication.Models
{
    public class ContactModel
    {
        public int ID { get; set; }

        [DisplayName("Labels.FirstName")]
        [Required(ErrorMessage = "Errors.Required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Errors.MaxLength")]
        public required string FirstName { get; set; }

        [DisplayName("Labels.LastName")]
        [Required(ErrorMessage = "Errors.Required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Errors.MaxLength")]
        public required string LastName { get; set; }

        [DisplayName("Labels.Phone")]
        [Required(ErrorMessage = "Errors.Required")]
        [RegularExpression(@"^(\+\d{1,3} ?)?\d{3} ?\d{3} ?\d{3}$", ErrorMessage = "Errors.InvalidFormat")]
        public required string Phone { get; set; }

        [DisplayName("Labels.Email")]
        [RegularExpression(@"^[^ @]+@[^ @]+\.[^ @]+$", ErrorMessage = "Errors.InvalidFormat")]
        [StringLength(50, ErrorMessage = "Errors.MaxLength")]
        public string? Email { get; set; }

        [DisplayName("Labels.City")]
        [StringLength(50, ErrorMessage = "Errors.MaxLength")]
        public string? City { get; set; }
    }
}
