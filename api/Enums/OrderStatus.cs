using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Enums
{
    public enum OrderStatus
    {
        Pending = 0,    // Order placed but not processed
        Confirmed = 1,  // Order confirmed by the seller
        Shipped = 2,    // Order shipped to the customer
        Delivered = 3,  // Order delivered successfully
        Canceled = 4,   // Order canceled by user or admin
        Refunded = 5    // Order refunded due to an issue
    }
}