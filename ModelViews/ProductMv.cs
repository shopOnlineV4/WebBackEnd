using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;


namespace ModelViews
{
    public class ProductMv
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string ProductImage { get; set; }
        public string Detail { get; set; }
        public decimal Price { get; set; }
        public Guid CreateBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateModified { get; set; }
        public Status Status { get; set; }
        public string ImageProductLocation { get; set; }
        public string FileData { get; set; }
        public IFormFile FileImage { get; set; }


        public CategoryMv Category { get; set; }
        public List<ImageMv> Images { get; set; }
        public List<TypeProductMv> TypeProducts { get; set; }
    }
}