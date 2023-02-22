using Microsoft.AspNetCore.Mvc;
using WebApiHTD1.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebApiHTD1.Controllers
{
    public class BookconsumeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            IEnumerable<BookModel> students = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7105/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Book");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BookModel>>();
                    readTask.Wait();

                    students = readTask.Result;
                }
                else //web api sent error response
                {
                    //log response status here..


                    students = Enumerable.Empty<BookModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(students);
        }
        public IActionResult IndexByid(int ID)
        {
            {

                IEnumerable<BookModel> students = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7105/api/");
                    //HTTP GET
                    var responseTask = client.GetAsync("Book?ID=" + ID);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<BookModel>>();
                        readTask.Wait();


                        students = readTask.Result;
                    }
                    else //web api sent error response
                    {
                        //log response status here..


                        students = Enumerable.Empty<BookModel>();

                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                return View(students);
            }
        }
        public IActionResult Details(BookModel obj,int ID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7105/api/Book");

                //HTTP POST
                var updateTask = client.PutAsJsonAsync<BookModel>("Book/Details", obj);
                updateTask.Wait();
                var result = updateTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Bookconsume");
                }
            }
            //ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(obj);
        }
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Insert(BookModel obj)
        {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7105/api/");
                    //HTTP post
                    var postTask = client.PostAsJsonAsync("Book/InsertData", obj);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View(obj);
        }
        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(BookModel obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7105/api/Book");

                //HTTP POST
                var updateTask = client.PutAsJsonAsync<BookModel>("Book/UpdateData", obj);
                updateTask.Wait();
                var result = updateTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index","Bookconsume");
                }
            }
            //ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(obj);
        }
        public ActionResult Delete(int ID, BookModel obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7105/api/");



                //HTTP DELETE
                //var deleteTask = client.DeleteAsync("Data/" +ID.ToString());
                //deleteTask.Wait();



                //var result = deleteTask.Result;
                var postTask = client.PostAsJsonAsync<BookModel>("Book/DeleteData?Id=" + ID, obj);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
       
    }
}
