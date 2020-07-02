using System;
using System.Collections.Generic;
using System.Text;

namespace ModelViews
{
    public class PagingModelView<T>
    {
        public int index { get; set; }
        public int Size { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }  
    }
}
