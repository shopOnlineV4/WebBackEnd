
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Entities
{
    public class ColorCode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorData { get; set; }

        public List<TypeProduct> typeProducts { get; set; }
    }
}
