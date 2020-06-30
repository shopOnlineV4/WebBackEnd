using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Entities
{
    public class Size
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<TypeProduct> typeProducts { get; set; }
    }
}
