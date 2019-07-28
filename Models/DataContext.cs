using Microsoft.EntityFrameworkCore;

namespace helpdeskAPI.Models
{
    public class DataContext: DbContext
    {
        //constructor
        public DataContext(DbContextOptions<DataContext> options)
        :base(options)
        {
        }

        public DbSet<mData> mDatas {get; set;}
    }
}