using System;
using System.Collections.Generic;

namespace Domain.Models.Entities
{
    public class TypeProduct
    {
        public Guid Id { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public Guid ProductId { get; set; }
    }
}