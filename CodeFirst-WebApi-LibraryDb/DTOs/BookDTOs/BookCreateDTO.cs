namespace CodeFirst_WebApi_LibraryDb.DTOs.BookDTOs
{
    public class BookCreateDTO : BookBaseDTO
    {
        public string AuthorName { get; set; }
        public List<string> Types { get; set; }

        public BookCreateDTO()
        {
            Types = new List<string>();
        }

    }
}
