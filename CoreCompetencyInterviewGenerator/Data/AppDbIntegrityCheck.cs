using Microsoft.EntityFrameworkCore;

namespace InterviewGeneratorBlazorHybrid.Data
{
    internal class AppDbIntegrityCheck
    {
        private readonly AppDbContextFactory _contextFactory;
        public AppDbContext _context { get; set; }
        public AppDbIntegrityCheck(AppDbContextFactory contextFactory) {
            _contextFactory = contextFactory;
        }
        public bool IsValidDatabase()
        {
            try
            {
                if (!TestIfDatabaseExists() || !TestIfRequiredTablesExist())
                    return false;
            }
            catch
            {
                // If this thing isn't even a database.
                return false;
            }

            return true;
        }

        public bool TestIfDatabaseExists()
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                // Try to open a connection to the database
                if (!context.Database.CanConnect())
                    return false;

                // Check if required tables exist
                if (!TestIfRequiredTablesExist())
                    return false;

                return true;
            }
            catch
            {
                // If anything fails along the way
                return false;
            }
        }

        public bool TestIfRequiredTablesExist()
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                // Collect table names from EF model
                var tableNames = context.Model
                    .GetEntityTypes()
                    .Select(et => et.GetTableName())
                    .Where(name => !string.IsNullOrEmpty(name))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                // Query sqlite_master for existing user tables (ignore sqlite_ internal tables)
                var existingTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                var connection = context.Database.GetDbConnection();
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        existingTables.Add(reader.GetString(0));
                    }
                }
                connection.Close();

                var missing = tableNames.Where(t => !existingTables.Contains(t)).ToArray();
                return missing.Length == 0;
            }
            catch (Exception)
            {
                // On any error treat as failure and return none of the tables verified
                return false;
            }
        }
    }
}
