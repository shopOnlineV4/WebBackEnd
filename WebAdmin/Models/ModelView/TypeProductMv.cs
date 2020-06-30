using System;
using System.Collections.Generic;

namespace WebAdmin.Models.ModelView

{
    public class TypeProductMv
    {
        public Guid Id { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public Guid ProductId { get; set; }
        public SizeMv Size { get; set; }
        public ColorCodeMv ColorCode { get; set; }
        public ProductMv Product { get; set; }
        public List<OrderDetailMv> OrderDetails { get; set; }
    }
}