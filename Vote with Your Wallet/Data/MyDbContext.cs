using Microsoft.EntityFrameworkCore;
using System;
namespace Vote_with_Your_Wallet.Data
{
	public class MyDbContext : DbContext
	{
		public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
		{
		}

		public DbSet<Cause> Causes { get; set; }
	}
}
