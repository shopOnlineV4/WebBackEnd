using Domain.Models.Enum;
using System;
using System.Collections.Generic;

namespace WebApi.Models.ModelView
{
    public class CategoryMv
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TypeCategories CategoryParent { get; set; }
        public Guid SubCategoryId { get; set; }
        public CategoryMv CategoryDataParent { get; set; }
        public List<CategoryMv> ListChilds { get; set; }
        public List<ProductMv> Products { get; set; }
        
      
    }
}