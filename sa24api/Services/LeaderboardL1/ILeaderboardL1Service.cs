using sa24api.Models;

namespace sa24api.Services.LeaderboardL1
{
    public interface ILeaderboardL1Service
    {
        public Task<IEnumerable<GameLevel1Dto>> GetClassificationLevel1();
        public Task<int> InsertGameLevel1Async(GameLevel1Dto game);
        public Task<int> DeleteGameLevel1Async(int gameId);
    }
}
