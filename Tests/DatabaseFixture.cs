using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{

    public class DatabaseFixture : IDisposable
    {
        public myDBContext Context { get; private set; }

        public DatabaseFixture()
        {

            // Set up the test database connection and initialize the context
            var options = new DbContextOptionsBuilder<myDBContext>()

                .UseSqlServer("Server=LAPTOP-LDNABVH4\\SQLEXPRESS;Database=Tests_329239529;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options;
            Context = new myDBContext(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            // Clean up the test database after all tests are completed
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
