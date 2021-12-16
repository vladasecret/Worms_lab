using Microsoft.EntityFrameworkCore;
using Worms_lab.Data;

namespace WormsTest.DbBehaviorTests
{
	internal class TestDbContextFactory : IDbContextFactory<EnvironmentContext>
	{
		private DbContextOptions<EnvironmentContext> _options;

		public TestDbContextFactory(DbContextOptions<EnvironmentContext> options)
		{

			_options = options;
		}

		public EnvironmentContext CreateDbContext()
		{
			return new EnvironmentContext(_options);
		}
	}

}
