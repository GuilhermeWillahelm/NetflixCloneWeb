using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetflixCloneWeb.Models;

namespace NetflixCloneWeb.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set;}
        public DbSet<Episode> Episodes { get; set;}
        public DbSet<Series> Series { get; set;}
        public DbSet<MyList> MyLists { get; set;}
        public DbSet<User> Users { get; set;}
        public DbSet<UserLogin> UserLogins { get; set;}
    }
}
