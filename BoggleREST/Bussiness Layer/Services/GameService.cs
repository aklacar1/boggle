using BoggleREST.API.ServiceInterfaces;
using BoggleREST.Helpers;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoggleREST.Bussiness_Layer.Services
{
    internal partial class GameService : IGameService
    {
        private readonly BoggleContext dbContext;

        List<List<string>> dice = new List<List<string>>(){new List<string>(){"R","I","F","O","B","X"},
                                                           new List<string>(){"I","F","E","H","E","Y"},
                                                           new List<string>(){"D","E","N","O","W","S"},
                                                           new List<string>(){"U","T","O","K","N","D"},
                                                           new List<string>(){"H","M","S","R","A","O"},
                                                           new List<string>(){"L","U","P","E","T","S"},
                                                           new List<string>(){"A","C","I","T","O","A"},
                                                           new List<string>(){"Y","L","G","K","U","E"},
                                                           new List<string>(){"Qu","B","M","J","O","A"},
                                                           new List<string>(){"E","H","I","S","P","N"},
                                                           new List<string>(){"V","E","T","I","G","N"},
                                                           new List<string>(){"B","A","L","I","Y","T"},
                                                           new List<string>(){"E","Z","A","V","N","D"},
                                                           new List<string>(){"R","A","L","E","S","C"},
                                                           new List<string>(){"U","W","I","L","R","G"},
                                                           new List<string>(){"P","A","C","E","M","D"}};


        public GameService(BoggleContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddWord(long roomId, string word)
        {
            GameRoom gr = dbContext.GameRoom.Find(roomId);
            if (gr.EndTime != null)
                return false;
            GameWords gw = new GameWords();
            gw.Word = word; // TODO Validate word
            gw.UserId = dbContext.GetCurrentUserId();
            gw.GameRoomId = roomId;
            dbContext.GameWords.Add(gw);
            dbContext.SaveChanges();
            return true;
        }

        public List<GameLetters> CreateGameRoom() {
            GameRoom gr = new GameRoom();
            dbContext.GameRoom.Add(gr);
            dbContext.SaveChanges();
            Random random = new Random();
            for (int i = 0; i < 16; i++) {
                GameLetters gl = new GameLetters();
                gl.GameRoomId = gr.Id;
                gl.Position = i;
                gl.Letter = dice[i][random.Next(0, 6)]; //TODO Random letter from table

                dbContext.GameLetters.Add(gl);
                dbContext.SaveChanges();
            }
            

            GameParticipants gp = new GameParticipants();
            gp.GameRoomId = gr.Id;
            gp.UserId = dbContext.GetCurrentUserId();
            dbContext.GameParticipants.Add(gp);
            dbContext.SaveChanges();



            return dbContext.GameLetters.Where(x => x.GameRoomId == gr.Id).ToList();
        }

        public List<GameLetters> GetGameRoomLettersByRoomId(long roomId)
        {
            return dbContext.GameLetters.Where(x => x.GameRoomId == roomId).ToList();
        }

        public List<string> GetGameRoomParticipantsByRoomId(long roomId)
        {
            return dbContext.GameParticipants.Where(x => x.GameRoomId == roomId).Select(x => x.User.UserName).ToList();
        }

        public GameRoom GetRoomStatusByID(long roomId)
        {
            GameRoom gr = dbContext.GameRoom.Find(roomId);
            return gr;
        }

        public bool JoinGameRoom(long roomId)
        {
            GameRoom gr = dbContext.GameRoom.Find(roomId);
            if (gr.StartTime != null)
                return false;
            GameParticipants gp = new GameParticipants();
            gp.GameRoomId = roomId;
            gp.UserId = dbContext.GetCurrentUserId();
            dbContext.GameParticipants.Add(gp);
            dbContext.SaveChanges();
            return true;
        }

        public bool StartGame(long roomId)
        {
            GameRoom gr = dbContext.GameRoom.Find(roomId);
            gr.StartTime = DateTime.Now;
            dbContext.GameRoom.Update(gr);
            dbContext.SaveChanges();
            BackgroundJob.Schedule(() => DistributeResults(roomId), TimeSpan.FromMinutes(3));
            return true;
        }


        // TODO : Remove duplicate words
        public bool DistributeResults(long roomId)
        {
            GameRoom gr = dbContext.GameRoom.Find(roomId);
            gr.EndTime = DateTime.Now;
            dbContext.SaveChanges();
            List<GameParticipants> gameParticipants = dbContext.GameParticipants.Include(x=> x.User).Where(x => x.GameRoomId == roomId).ToList();
            Dictionary<string, List<string>> players = new Dictionary<string, List<string>>();
            foreach (GameParticipants gameParticipant in gameParticipants)
            {
                players
                    .Add(gameParticipant.User.UserName, 
                        dbContext.GameWords.Where(x => x.UserId == gameParticipant.UserId && x.GameRoomId == roomId)
                        .Select(x => x.Word)
                        .ToList());
            }

            Dictionary<string, int> results = Utils.ScorePlayers(players);
            var usernames = players.Keys.ToList();
            var tokens = dbContext.FirebaseTokens.Where(x => usernames.Contains(x.User.UserName)).Select(x => x.Token).ToList();
            foreach (string token in tokens) {
                Utils.SendNotificationAsync(token, "Game Results", JsonConvert.SerializeObject(results));
            }
            return true;
        }
    }
}
