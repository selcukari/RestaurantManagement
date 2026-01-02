using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Bus.Events
{
    public record ProductNameChangedEvent(Guid ProductId, string UpdatedName);
}
