using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiHTD1.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebApiHTD1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly DataBaseContext _context;
        public DataController(DataBaseContext context)
        {
            _context = context;
        }
       
        [HttpGet]
        public IEnumerable<APIData> Get()
        {
            return _context.APIData.ToList();
        }
        //[HttpGet]
        //[Route("Details")]
        //public APIData Details(int Id)
        //{
        //    APIData obj = new APIData();
        //    obj = _context.APIData.Find(Id);
        //    return obj;
        //}
        [HttpGet]
        [Route("Get")]
        public IEnumerable<APIData> Get(int Id)
        {
            return _context.APIData.Where(s => s.Id == Id);
        }
        //[HttpGet]

        //public APIData Get(int Id)
        //{
        //    var res = _context.APIData.Find(Id);
        //    return res;
        //}
        //[HttpGet]
        //public IEnumerable<APIData> Get(int Id, string Gender)
        //{
        //    var result = from s in _context.APIData where s.Id == Id && s.Gender == Gender select s;
        //    var res = _context.APIData.Find(Id);
        //    return result;
        //}
        //public IEnumerable<APIData> Get(int Id, string Name)
        //{
        //    return _context.APIData.Where(s => s.Id == Id && s.Name == Name);
        //}
        //[HttpPost]
        //[Route("InsertData")]
        //public bool InsertData(APIData obj)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        _context.APIData.Add(obj);
        //        _context.SaveChanges();
        //        return true;
        //    }
        //}
        //[HttpPost]
        //[Route("InsertDatanew")]
        //public bool InsertDatanew(APIData obj)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        _context.APIData.Add(obj);
        //        _context.SaveChanges();
        //        return true;
        //    }

        //}

        [Route("UpdateData")]
        public bool UpdateData(APIData obj)
        {
            if (ModelState.IsValid)
            {
                _context.Update(obj);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        [Route("DeleteData")]
        public bool DeleteData(int Id)
        {
            var result = _context.APIData.Find(Id);
            if (result != null)
            {
                _context.APIData.Remove(result);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
      

    }
}
