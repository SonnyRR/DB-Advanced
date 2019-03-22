using System.ComponentModel.DataAnnotations;

namespace FastFood.Web.ViewModels.Employees
{
    public class RegisterEmployeeInputModel
    {
        [Required]
        public string Name { get; set; }

        [Range(16, 65)]
        public int Age { get; set; }

        public int PositionId { get; set; }

        public string PositionName { get; set; }

        [Required]
        [MaxLength(80)]
        public string Address { get; set; }
    }
}
