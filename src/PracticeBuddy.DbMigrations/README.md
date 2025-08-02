# README

Don't forget to initialize user secrets!
`dotnet user-secrets --project src/PracticeBuddy.DbMigrations init`
`dotnet user-secrets --project src/PracticeBuddy.DbMigrations set PracticeBuddyApi:MySql:ConnectionString "Server=127.0.0.1;Database=practicebuddy-db;Uid=root;Pwd=Password123;"`

To apply migrations:
dotnet run --project src/PracticeBuddy.DbMigrations
dotnet run --project src/PracticeBuddy.DbMigrations -- -t migrate:down
dotnet run --project src/PracticeBuddy.DbMigrations -- -t rollback:all