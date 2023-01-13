﻿namespace Domain.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Product>? Products { get; set; }

    }  
    
    public class CategoryDto 
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        

    }
}
