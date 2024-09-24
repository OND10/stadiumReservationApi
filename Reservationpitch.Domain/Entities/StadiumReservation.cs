using Reservationpitch.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Entities
{
    public class StadiumReservation : AuditableEntity
    {
        public string UserName {  get; set; }
        public string UserPhoneNumber {  get; set; }
        public DateTime Date {  get; set; }
        public float Price {  get; set; }
        public string StadiumCenterPhoneNumber {  get; set; }
        public string Payment {  get; set; }
    }
}
