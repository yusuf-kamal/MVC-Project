using System.ComponentModel.DataAnnotations;
using System;

namespace Demo.Pl.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
