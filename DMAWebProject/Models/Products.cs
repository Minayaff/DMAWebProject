﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMAWebProject.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        [ValidateNever]
        public string ImgUrlBase { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public List<Order> Orders { get; set; }
        [ValidateNever]
        public List<Images> Images { get; set; }
        [NotMapped]
        [Required]
        public IFormFile ImgUrlBaseFile { get; set; }
        [NotMapped]
        [ValidateNever]
        public List<IFormFile> ImagesFiles { get; set; }


    }
}
