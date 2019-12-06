using System;
using Microsoft.EntityFrameworkCore;
using YourSpace.Models;

namespace YourSpace.Data
{
    public class PhotoContext : DbContext
    {
        public PhotoContext(DbContextOptions<PhotoContext> options)
           : base(options)
        {
        }

        public DbSet<Photos> Photo { get; set; }
    }
}
