using LibraryUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryUI.Controllers
{
    public class AuthorController : Controller
    {
        private readonly string baseAddress = "https://localhost:7019/api/";
        public IActionResult Index()
        {
            List<AuthorVM> authors;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                var response = client.GetAsync("Author/GetAllAuthors");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadFromJsonAsync<List<AuthorVM>>();
                    read.Wait();
                    authors = read.Result;
                    return View(authors);
                }
                else { return NotFound(); }
            }
        }
        public IActionResult Details(int id)
        {
            AuthorVM author = new();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                var response = client.GetAsync($"Author/AuthorDetail?id={id}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readauthor = result.Content.ReadFromJsonAsync<AuthorVM>();
                    readauthor.Wait();
                    author = readauthor.Result;
                    return View(author);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AuthorCreateVM authorCreateVM)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    var response = client.PostAsJsonAsync("Author/CreateAuthor", authorCreateVM);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(authorCreateVM);
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            AuthorVM author = new();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                var response = client.GetAsync($"Author/AuthorDetail?id={id}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadFromJsonAsync<AuthorVM>();
                    read.Wait();
                    author.FirstName = read.Result.FirstName;
                    author.LastName = read.Result.LastName;

                    return View(author);
                }
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Edit(AuthorVM authorVM)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                var response = client.PutAsJsonAsync<AuthorVM>("Author/UpdateAuthor", authorVM);
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(authorVM);
            }
        }
    }
}
