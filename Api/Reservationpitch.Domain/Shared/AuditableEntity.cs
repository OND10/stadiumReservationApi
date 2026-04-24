namespace Reservationpitch.Domain.Shared
{
    public class AuditableEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
    }
}
