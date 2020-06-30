using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WebAdmin.Models.ModelView
{
    public class UserMv 
    {
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
}