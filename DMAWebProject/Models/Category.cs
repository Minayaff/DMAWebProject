﻿namespace DMAWebProject.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public bool IsActive { get; set; }
        public List<Products>  Products { get; set; }
    }
}
