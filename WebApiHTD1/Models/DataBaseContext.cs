using Microsoft.EntityFrameworkCore;
namespace WebApiHTD1.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
        {

        }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        public virtual DbSet<APIData> APIData { get; set; }
        public virtual DbSet<BookModel> BOOK { get; set; }
    }
}