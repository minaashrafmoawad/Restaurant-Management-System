﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Domain.Enums
{
    public enum OrderStatus
    {
        Pending,
        Preparing,
        Ready,
        OutForDelivery,
        Completed,
        Cancelled
    }
}
