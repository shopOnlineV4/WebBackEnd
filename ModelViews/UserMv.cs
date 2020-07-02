
using System;
using System.Collections.Generic;

namespace ModelViews
{
    public class UserMv
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string PortalCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public List<CartMv> Carts { get; set; }
        public List<OrderMv> Orders { get; set; }
    }

    public class LogingModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserInfor
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}