﻿using FullStackApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStackApi.Data
{
    public class FullStackDbContext : DbContext
    {
        public FullStackDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
