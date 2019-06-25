using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoggleREST.API.ServiceInterfaces
{
    public interface IGameService
    {
        List<GameLetters> CreateGameRoom();
        List<GameLetters> GetGameRoomLettersByRoomId(long roomId);
        List<string> GetGameRoomParticipantsByRoomId(long roomId);
        bool JoinGameRoom(long roomId);
        bool StartGame(long roomId);
        bool AddWord(long roomId, string word);
    }
}
