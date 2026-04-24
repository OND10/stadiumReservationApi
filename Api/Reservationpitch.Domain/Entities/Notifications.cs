using Reservationpitch.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Entities
{
    public class Notifications : AuditableEntity     
    {
       public string senderId { get; set; }
       public string receiverId {  get; set; }
       public string Message {  get; set; }
       public bool isRead {  get; set; } 

    }
}
