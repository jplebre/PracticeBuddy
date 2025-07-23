# README

This is an application that tries to manage an users practice time by weighing the importance of exercise variation, according to the best written practices at the time. For example, it doens't just "randomize" today's exercise, but weighs which exercise would be more efficient for you, based on:
- Repetition: regular repetition makes things stick better. Don't go too long without practicing something, specially if you are just beginning to learn it.
- Keep you on the challenge curve: if certain exercises are too easy, push up the difficulty, or weigh him lower in the rotation (upkeep, rather than practice)
- etc.

### Local Development
#### Using dotnet secrets
If you've never used .net secrets before, initialize with 
- `dotnet user-secrets --project src/PracticeBuddy.API init`.
- `dotnet user-secrets --project src/PracticeBuddy.API set PracticeBuddyApi:MySql:ConnectionString "Server=127.0.0.1;Database=practicebuddy-db;Uid=root;Pwd=Password123;"`

Note: the Db Migration project will also need a the above secrets setup.

#### Helpful Scripts
`dotnet build` - builds everything
`dotnet run --project src/PracticeBuddy.API` - runs API locally
`dotnet test` - runs all tests (which will fail, as some will need the database setup)

The easy way, is to just use docker compose.
It'll boot up a db, do health checks, and let you watch the containers.
`docker compose --file compose.dev.yml up` to start all the service

**DON'T FORGET TO SPIN THE CONTAINERS DOWN WHEN YOU ARE DONE**
`docker compose --file compose.dev.yml down --remove-orphans`
`docker compose --file compose.dev.yml stop`
`docker compose --file compose.dev.yml rm -sf`

#### Database
If you want to use MySqlWorkbench, VSCode plugin or CLI, you need have the environment variable `MYSQL_ROOT_HOST: "%"` set in dockercompose. You also need to have the port 3306 exposed with `expose: 3306`


#### Docker
`docker build -t practicebuddy-api-image -f src/PracticeBuddy.API/Dockerfile .` - Build API dockerimage

You can then run it by:
1. `docker create --name practicebuddy-api practicebuddy-api-image` - create container
2. `docker start practicebuddy-api` - start container
3. `docker attach --sig-proxy=false practicebuddy-api` - if you want to attach to container, verify resource running/see logs
4. `docker stop practicebuddy-api` - stop container

or, instead of steps [2-4]:
1. `docker run -it --rm --name practicebuddy-api -p 5068:8080 practicebuddy-api-image` - runs in shell (use CTRL+C to stop). Also, deletes containers after stopping

Check it works by navigating to http://localhost:5068/health

Finally, you can verify containers are gone with `docker ps -a`.
If things are lingering, you can remove everything with `docker system prune -a` 
**(WARNING: THIS DELETES ALL CONTAINERS/IMAGES/RESOURCES)**

