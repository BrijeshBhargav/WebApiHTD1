using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Newtonsoft.Json;
using System.Xml.Linq;
using WebApiHTD1.Models;
namespace WebApiHTD1.Controllers
{
    public class consumeoneparController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            IEnumerable<APIData> students = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7105/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Data");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<APIData>>();
                    readTask.Wait();

                    students = readTask.Result;
                }
                else //web api sent error response
                {
                    //log response status here..


                    students = Enumerable.Empty<APIData>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(students);
        }
        public IActionResult IndexByid(int Id)
        {
            {

                IEnumerable<APIData> students = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7105/api/");
                    //HTTP GET
                    var responseTask = client.GetAsync("Data?Id=" + Id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<APIData>>();
                        readTask.Wait();


                        students = readTask.Result;
                    }
                    else //web api sent error response
                    {
                        //log response status here..


                        students = Enumerable.Empty<APIData>();

                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                return View(students);
            }
        }
        public IActionResult Index1(int Id, string Name)
        {
            {
                IEnumerable<APIData> students = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7105/api/");
                    //HTTP GET
                    var responseTask = client.GetAsync("Data?Id= " + Id + "&" + "Name=" + Name);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<APIData>>();
                        readTask.Wait();
                        students = readTask.Result;
                    }
                    else //web api sent error response
                    {
                        //log response status here..
                        students = Enumerable.Empty<APIData>();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                return View(students);
            }
        }
        [HttpGet]
        public ActionResult insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult insert(APIData obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7105/api/Data");
                //HTTP post
                var postTask = client.PostAsJsonAsync("Data/InsertDatanew", obj);
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
        public IActionResult Edit(APIData obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7105/api/Data");

                //HTTP POST
                var updateTask = client.PutAsJsonAsync<APIData>("Data/UpdateData", obj);
                updateTask.Wait();
                var result = updateTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "consumeonepar");
                }
            }
            //ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(obj);
        }

        public ActionResult Delete(int Id, APIData obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7105/api/");



                //HTTP DELETE
                //var deleteTask = client.DeleteAsync("Data/" +ID.ToString());
                //deleteTask.Wait();



                //var result = deleteTask.Result;
                var postTask = client.PostAsJsonAsync<APIData>("Data/DeleteData?Id=" + Id, obj);
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
 