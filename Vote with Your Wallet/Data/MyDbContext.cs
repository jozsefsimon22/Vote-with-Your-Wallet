using Microsoft.EntityFrameworkCore;
using System;
namespace Vote_with_Your_Wallet.Models
{
	public class MyDbContext : DbContext
	{
        // Constructor that takes DbContextOptions as a parameter and passes it to the base class
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
		{
		}

        // Define a DbSet for the Cause model to manage causes in the database
        public DbSet<Cause> Causes { get; set; }

        // Define a DbSet for the Signatures model to manage signatures in the database
        public DbSet<Signatures> Signatures { get; set; }

        // Define a DbSet for the User model to manage users in the database
        public DbSet<User> Users { get; set; }

	}
}
