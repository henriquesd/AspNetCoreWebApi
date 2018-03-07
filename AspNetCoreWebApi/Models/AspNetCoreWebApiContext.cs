using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Models
{
    public class AspNetCoreWebApiContext : DbContext
    {
        public AspNetCoreWebApiContext(DbContextOptions<AspNetCoreWebApiContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
