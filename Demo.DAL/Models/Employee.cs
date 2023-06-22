using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is Required")]
        [MaxLength(50,ErrorMessage ="Max Chars is 50 Chars")]
        [MinLength(5,ErrorMessage ="Min Chars is 50 Chars")]
        public string Name { get; set; }
        [Range(18,80)]
        public int? Age { get; set; }
        [Required(ErrorMessage ="Enter your Address")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; }= DateTime.Now;
        public string ImageUrl { get; set; }





    }
}
