using CSharpFunctionalExtensions;

namespace YoutubeMusicPlayer.MusicManagement
{
    public static class DbMigrator
    {
        public static Result SetupDb(string connectionString)
        {
            var upgrader = DbUp.DeployChanges.To
                .SQLiteDatabase(connectionString)
                .LogToConsole()
                .WithTransaction()
                .WithScriptsEmbeddedInAssembly(typeof(DbMigrator).Assembly)
                .Build();

            var result = upgrader.PerformUpgrade();

            return Result.SuccessIf(result.Successful,result?.Error?.Message);
        }
    }

}