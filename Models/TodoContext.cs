using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics.CodeAnalysis;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext() : base()
        {
        }

        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
            if (!TablesReadyForUse())
            {
                var databaseCreator = (RelationalDatabaseCreator)Database.GetService<IDatabaseCreator>();
                databaseCreator.CreateTables();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine("");
            var connStr = "Data Source = localhost,57000; Initial Catalog=todoitem; User Id = SA; Password = Str#ng_Passw#rd;MultipleActiveResultSets=true; TrustServerCertificate = true";
            optionsBuilder.UseSqlServer(connStr);
        }


        bool TablesReadyForUse()
        {
            try
            {
                var first = this.TodoItems.FirstOrDefault();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public DbSet<TodoItem> TodoItems { get; set; } = null!;
    }
}

