using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetflixCloneWeb.Models;
using NetflixCloneWeb.Dtos;

namespace NetflixCloneWeb.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Video> Vídeos { get; set;}
        public DbSet<MyList> MyLists { get; set;}
        public DbSet<User> Users { get; set;}
        public DbSet<UserLogin> UserLogins { get; set;}
    }
}
