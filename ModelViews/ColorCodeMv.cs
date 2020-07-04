using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelViews
{
    public class ColorCodeMv
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorData { get; set; }
    }
    public class ColorInput
    {
        public string Name { get; set; }
        public string ColorData { get; set; }
    }
}