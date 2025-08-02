# TODO

- Make docker images when building project (by using csproj, see fluent migration project)
  - Docs here https://learn.microsoft.com/en-us/visualstudio/containers/container-msbuild-properties?view=vs-2022
  - and here https://learn.microsoft.com/en-us/visualstudio/containers/docker-compose-properties?view=vs-2022

Decisions:
- Go with exposed endpoints for now (Add oauth later if you ever make an app)
- You don't need the UserController yet. This is more for other things. 
  - Remember REST works with collections "in context", so if you 
  - have an user ID, in a JWT or whatever, then it knows that is "get all exercises for this user"

Remember that an "exercise" needs to actually create "ExerciseInstances".
Eg: if you are practicing the C Major and A Minor Scale, both hands parallel and contrary motion, then that exercise should have:
- An exercise instance of C Major parallel (both hands)
- An exercise instance of C Major Contrary (both hands)
- An exercise instance of A Minor parallel (both hands)
- An exercise instance of A Minor Contrary (both hands)

THe exercise instance is what tracks what BPM each mode is at, and how many times have you practiced C Major vs A Minor.

Exercise Instance
- Key
- Variation
- PracticeCount
- CurrentBPM
- MaxBPM
- DateStarted
- LastPracticed
- ComfortRating


- COncept of "flow"
  - Flow happens when a user reached the goal for a exercise instance (C Major scale both hands at 100 with all the different variants), then it could get "promoted" or added to a next exercise.
  - eg.: 2 Octave C Major Scale @ 100bpm > 2 Octave C Major Scale inverse motion @ 100bpm > Grand Scale Format C Major Scale @ 100bpm