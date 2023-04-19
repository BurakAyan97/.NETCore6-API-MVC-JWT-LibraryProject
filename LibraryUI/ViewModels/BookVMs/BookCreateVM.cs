namespace LibraryUI.ViewModels.BookVMs
{
    public class BookCreateVM : BookBaseVM
    {
        public string AuthorName { get; set; }
        public List<string> Types { get; set; }
        public BookCreateVM()
        {
            Types = new List<string>();
        }
    }
}
