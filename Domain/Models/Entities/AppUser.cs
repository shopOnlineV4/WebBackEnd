using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain.Models.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string PortalCode { get; set; }

       
       
    }
}