
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAdmin.Models.Enum;

namespace WebAdmin.Models.ModelView
{
    public class CategoryMv
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Can not null! ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Can not null! ")]
        public TypeCategories CategoryParent { get; set; }
        public Guid SubCategoryId { get; set; }
        public List<ProductMv> Products { get; set; }
        public CategoryMv CategoryDataParent { get; set; }
        public List<CategoryMv> ListChilds { get; set; }


    }
}