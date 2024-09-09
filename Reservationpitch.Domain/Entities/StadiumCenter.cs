using Reservationpitch.Domain.Common;

namespace Reservationpitch.Domain.Entities
{
    public class StadiumCenter : AuditableEntity
    {
        public string Name { get; set; }
        public string PhoneNumber {  get; set; }
        public string Location {  get; set; }
        public string Owned_By { get; set; } = string.Empty;
        public bool DressingRoomVisible { get; set; } = false;
        public string ImageUrl { get; set; }

    }
}
