using Domain.Models.Enum;
using System;
using System.Collections.Generic;

namespace Domain.Models.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TypeCategories CategoryParent { get; set; }
        public Guid SubCategoryId { get; set; }
        public List<Product> Products { get; set; }
    }
}