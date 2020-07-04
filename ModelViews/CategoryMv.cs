
using ModelViews.Enum;
using System;
using System.Collections.Generic;

namespace ModelViews
{
    public class CategoryMv
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TypeCategories CategoryParent { get; set; }
        public Guid SubCategoryId { get; set; }
    }

    public class CategoryInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}