namespace CodeFirst_WebApi_LibraryDb.Entities
{
    public class Type : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<BookType> BookTypes { get; set;}

        public Type()
        {
            BookTypes = new HashSet<BookType>();
        }
    }
}
