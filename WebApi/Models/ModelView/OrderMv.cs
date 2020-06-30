﻿using Domain.Models.Enum;
using System;
using System.Collections.Generic;

namespace WebApi.Models.ModelView{
    public class OrderMv
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public PayWays PayWays { get; set; }
        public double Amount { get; set; }
        public int StatusOrder { get; set; }

        public UserMv AppUser { get; set; }
        public List<OrderDetailMv> OrderDetails { get; set; }
    }
}