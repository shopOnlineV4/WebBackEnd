using System;

namespace ModelViews
{
    public class CartMv
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime DateAdd { get; set; }
        public int Quantity { get; set; }


        public UserMv AppUser { get; set; }
       
    }
}