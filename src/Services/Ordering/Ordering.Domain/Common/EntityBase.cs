namespace Ordering.Domain.Common
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string? LastModifyBy { get; set; }
        public DateTime LastModifyOn { get; set; }
    }
}
