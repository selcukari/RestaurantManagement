using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Bus.Events
{
   public record ProductPictureUploadedEvent(Guid CourseId, string ImageUrl);
   
}
