using Microsoft.EntityFrameworkCore;
using PostBackend.Models;


namespace PostBackend.Data
{
    public class DataContext : DbContext
    {
       
        public DbSet<PostModel> Posts { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        { }


    }

}
