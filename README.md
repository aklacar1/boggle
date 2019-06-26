# Boggle

Still needs, a lot of validation (word validation, word removal on duplicates...), expection handling, async handling...
Main thing to resolve next is Firebase.
User Handling is a bit older code, so not all parts of it work
Mail Sending disabled, needed manual activation.


# What to do in order to start the game, easy way:
1. First thing needed is proper SQL Database Server, and connection string for that DB
2. Set connection string in appsettings.json File
3. Uncomment these lines in startup of .NET Core project:
           //dBContext.Database.EnsureDeleted();
           //dBContext.Database.EnsureCreated();
4. Run .NET Core backend once.
5. Now you can STOP the project and comment the same lines from STEP 3 again.
6. Run BoggleREST Project
7. Run BoggleWEB Project (ng serve --open)
8. Considering that User Management is not finished, use swagger from BoggleREST site that was opened in order to register new user.
9. Manually in Database set the User as Confirmed Email - Email Service not finished, it used to work before as it is, but currently buggy.
10. You can now Log into the Angular Frontend and create new Room.

# What is implemented, and what is missing (Backend):
1. First Assigment, Task 1: Implemented as static method in Utils: ScorePlayer. Missing testing and configuration file. Currently hardcoded
2. First Assigment, Task 2: Utils.ScorePlayers - Missing Testing
3. Max play time implemented (3 MIN), however not validated.
4. In general Custom Exception handling is missing everywhere. Game does not validate at all.
5. Missing requirement : (If two or more players wrote the same word, it is removed from all players lists)
6. All values need configuration files, since they are mainly hardcoded. Dice set for English version, but words never validated.
7. Scoring from First assigment utilized
8. Firebase with Background Tasks used for Score handling, game ending and player notifications.

# Frontend:
1. Validation missing on Frontend also
2. Counter missing
3. Proper Firebase notification handling missing - Currently written in console
4. Sign Up Page not implemented, just a placeholder
5. Tests where necessary missing - Not much need for this since there is no logic
6. Entire frontend needs rework on UI/UX part also optimization needed on both frontend and backend.


How the game should work currently (Some parts not even manually tested):

1. Player A Creates a Game Room X
2. Player A sends to Player B link to game Room
3. Player B joins Game Room X (Should only be able to join if game not started at all - Not implemented)
4. Any of the 2 players can start the game (Should be only possible by Player A)
5. Notification Handling for game start - Not implemented, currently other player will not know if game started.
6. Game start button should be removed and validated after game starts - Not implemented
7. Countdown of 3 Minutes missing
8. When game finishes, notification is pushed to each participating player about results (Only console write, not implemented proper handling)


How the game would work with more development time:
1. Player A creates Game Room X
2. Player A sends only Game Room ID to player B, player B opens room with ID
3. Any player or Player A can start game (not sure which is better)
4. On all participating players Accounts, countdown is started for that game.
5. Each player sends words to Backend, and their words are shown on frontend.
6. No player can join game after it was started.
7. After game finishes, each players game screen changes and shows results. These results can be accessed always with Room ID. 
8. All words are validated, and no duplicates are allowed, or rather later removed.
9. Both solutions are dockerized (Not necessary, but nice to have) and deployed using GCloud and Kubernetes

