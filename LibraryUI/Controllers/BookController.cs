using LibraryUI.ViewModels.BookVMs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryUI.Controllers
{
    public class BookController : Controller
    {
        private readonly string baseAdress = "https://localhost:7019/api/";
        public IActionResult Index()
        {
            List<BookVM> books = new();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAdress);
                var response = client.GetAsync("Book/GetAllBooks");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadFromJsonAsync<List<BookVM>>();
                    read.Wait();
                    books = read.Result;
                    return View(books);
                }
            }
            return NotFound();
        }

        public IActionResult Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAdress);
                var response = client.GetAsync($"Book/BookDetail?id={id}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadFromJsonAsync<BookVM>();
                    readData.Wait();
                    return View(readData.Result);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormCollection collection)
        {
            BookCreateVM bookCreateVM = new()
            {
                Name = collection["Name"],
                AuthorName = collection["AuthorName"],
                Types = collection["type"].ToList()
            };
            
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAdress);
                    var response = client.PostAsJsonAsync<BookCreateVM>("Book/CreateBook", bookCreateVM);
                    response.Wait();
                    var result = response.Result;
                    string msg = result.Content.ReadAsStringAsync().Result;//Mesajı basmak istersek.
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(bookCreateVM);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
