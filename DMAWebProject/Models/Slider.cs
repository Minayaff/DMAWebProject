using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMAWebProject.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required]
        public string SubTitle { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        [ValidateNever]
        public IFormFile file { get; set; }

        public bool IsActive { get; set; } = true   ;
    }
}
