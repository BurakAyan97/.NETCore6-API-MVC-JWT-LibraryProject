namespace CodeFirst_WebApi_LibraryDb.Entities
{
    public class BookType : BaseEntity
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int TypeId { get; set; }
        public virtual Type Type { get; set; }
    }
}
