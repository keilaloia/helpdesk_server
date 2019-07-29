using Microsoft.EntityFrameworkCore;

namespace helpdeskAPI.Models
{
    public class DataContext: DbContext
    {
        public string ConnectionString {get; set;}
        //constructor
        public DataContext(DbContextOptions<DataContext> options)
        :base(options)
        {
        }
        public DbSet<mData> mDatas {get; set;}
    }
}