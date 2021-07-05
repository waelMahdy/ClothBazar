﻿using ClothBazar.Entity;
using ClothBazar.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModels
{
    public class OrdersViewModels
    {
       public List<Order> Orders { get; set; }
        public Pager Pager { get;  set; }
        public string Status { get;  set; }
        public string UserID { get; set; }

    }
    public class OrderDetailsViewModels
    {
        public Order Order { get; set; }
        public ApplicationUser OrderBy { get; set; }
        public List<string> AvailableStatuses { get; set; }
    }
}