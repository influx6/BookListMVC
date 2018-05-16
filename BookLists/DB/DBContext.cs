using BookLists.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLists.DB
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        { }

        public DbSet<Book> Books { get; set; }

        internal Task<Book> SingleOrDefaultAsync(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
