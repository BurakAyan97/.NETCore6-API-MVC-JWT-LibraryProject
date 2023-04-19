namespace CodeFirst_WebApi_LibraryDb.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<AuthorBook> AuthorBooks { get; set; }
        public ICollection<BookType> BookTypes { get; set; }
        public Book()
        {
            AuthorBooks = new HashSet<AuthorBook>();
            BookTypes = new HashSet<BookType>();
        }
    }
}
