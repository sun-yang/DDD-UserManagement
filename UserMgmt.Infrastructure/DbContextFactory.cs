using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgmt.Infrastructure
{
    public class DbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
           var builder = new DbContextOptionsBuilder<UserDbContext>();
            
            builder.UseSqlServer("server=localhost\\sqlexpress;database=ddd2;trusted_connection=true;");
            return new UserDbContext(builder.Options);
        }
    }
}
