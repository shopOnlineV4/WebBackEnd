using Domain.Models.Enum;
using System;
using System.Collections.Generic;

namespace Domain.Models.Entities
{
    public class Product
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
        public int CountView { get; set; }
        public int Status { get; set; }


     
    }
}