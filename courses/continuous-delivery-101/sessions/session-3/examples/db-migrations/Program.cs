using System;
using System.Reflection;
using DbUp;
using DbUp.Engine;

// Teaching reference for CD 101 — NOT a restored/buildable project.
// Build ONCE in CI (`dotnet publish`), then run the SAME published artifact against
// dev -> qa -> prod, passing only the connection string. Config travels with the
// artifact (CD minimum #9); the bytes never change between environments.
internal static class Program
{
    private static int Main(string[] args)
    {
        // The ONE thing that varies per environment: the connection string.
        // In CI it comes from Secrets Manager via OIDC, never a baked-in value.
        var connectionString =
            args.Length > 0 ? args[0] : Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            Console.Error.WriteLine("Usage: Migrator <connection-string>  (or set DB_CONNECTION_STRING)");
            return 2;
        }

        // Safe no-op if the database already exists; convenient for a fresh local DB.
        // On shared envs the database is pre-created and the deploy login is least-privilege
        // (no CREATE DATABASE / standing DDL), so this line simply no-ops there.
        EnsureDatabase.For.SqlDatabase(connectionString);

        var upgrader = DeployChanges.To
            .SqlDatabase(connectionString)
            // Ordered .sql files compiled INTO the assembly as embedded resources.
            // DbUp records each applied script in the SchemaVersions journal and skips
            // it next run — so re-running the last good deployment is safe (idempotent).
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        DatabaseUpgradeResult result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            // Non-zero exit FAILS the pipeline job — a red migration stops the line,
            // exactly like a failing build. The pipeline, not a human, gates promotion.
            Console.Error.WriteLine(result.Error);
            return 1;
        }

        Console.WriteLine("Schema up to date.");
        return 0;
    }
}
