﻿using MapTask.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MapTask.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }

}
