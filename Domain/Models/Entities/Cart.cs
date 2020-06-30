using System;

namespace Domain.Models.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime DateAdd { get; set; }
        public int Quantity { get; set; }


        public AppUser AppUser { get; set; }
       
    }
}