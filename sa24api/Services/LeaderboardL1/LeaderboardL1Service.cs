using Dapper;
using sa24api.Data;
using sa24api.Models;
using System.Data;

namespace sa24api.Services.LeaderboardL1
{
    public class LeaderboardL1Service : ILeaderboardL1Service
    {
        private readonly DataContext _dataContext;

        public LeaderboardL1Service(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<int> InsertGameLevel1Async(GameLevel1Dto game)
        {
            var sql = "insert into GamesLevel1 (UserId, Seconds) values " +
                "(@UserId, @Seconds)";
            using var connection = _dataContext.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("UserId", game.UserId);
            parameters.Add("Seconds", game.Seconds);
            var rowsInserted = await connection.ExecuteAsync(sql, parameters);
            if (rowsInserted == 0)
            {
                return 0;
            }

            var idGame = await GetLastInsertedGameLevel1IdAsync();

            foreach (var caparrot in game.Caparrots) 
            {
                int result = await InsertCaparrotIdentificatAsync(idGame, caparrot);
            }

            return idGame;
        }

        private async Task<int> InsertCaparrotIdentificatAsync(int gameId, string caparrot)
        {
            var sql = "insert into CaparrotsIdentificatsPerGame (IdGame, IdCaparrot) values " +
                "(@IdGame, @IdCaparrot)";
            using var connection = _dataContext.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("IdGame", gameId);
            parameters.Add("IdCaparrot", caparrot);
            var rowsInserted = await connection.ExecuteAsync(sql, parameters);
            if (rowsInserted == 0)
            {
                return 0;
            }
            return rowsInserted;
        }

        private async Task<int> GetLastInsertedGameLevel1IdAsync()
        {
            var sql = "SELECT MAX(Id) FROM GamesLevel1";
            using var connection = _dataContext.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql);
            if (id != 0)
            {
                return id;
            }
            else { return 0; }
        }

        public async Task<IEnumerable<GameLevel1Dto>> GetClassificationLevel1()
        {
            String sql = "SELECT GamesLevel1.Id, GamesLevel1.UserId, Usuaris.Nom AS UserName,"
                + " GamesLevel1.Seconds, GamesLevel1.CreatedAt, GamesLevel1.UpdatedAt"
                + " FROM GamesLevel1 INNER JOIN Usuaris ON GamesLevel1.UserId=Usuaris.Id"
                + " INNER JOIN (SELECT GamesLevel1.UserId, MAX(GamesLevel1.Seconds) AS MaxSeconds"
                + " FROM GamesLevel1"
                + " GROUP BY GamesLevel1.UserId) A ON GamesLevel1.UserId=A.UserId AND GamesLevel1.Seconds=A.MaxSeconds"
                + " WHERE Usuaris.Id > 6"
                + " ORDER BY GamesLevel1.Seconds DESC";
            using var connection = _dataContext.CreateConnection();
            var games = await connection.QueryAsync<GameLevel1Dto>(sql);
            
            foreach (var game in games)
            {
                game.Caparrots = await GetCaparrotsIdentificatsPerGameAsync(game.Id);
            }

            return games;
        }

        private async Task<List<string>> GetCaparrotsIdentificatsPerGameAsync(int gameId)
        {
            String sql = "SELECT IdCaparrot FROM CaparrotsIdentificatsPerGame " +
                "WHERE IdGame=@IdGame";

            var parameters = new DynamicParameters();
            parameters.Add("IdGame", gameId);

            using var connection = _dataContext.CreateConnection();
            var caparrots = await connection.QueryAsync<string>(sql, parameters);

            return caparrots.ToList();
        }

        public async Task<int> DeleteGameLevel1Async(int gameId)
        {
            int caparrotsDeleted = await DeleteCaparrotsIdentificatsPerGameAsync(gameId);

            if (caparrotsDeleted == 0)
            {
                // No havia caparrots identificats per aquesta partida
            }
            else
            {
                // S'han eliminat x caparrots
            }

            var sql = "DELETE FROM GamesLevel1 WHERE Id=@Id";
            using var connection = _dataContext.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("Id", gameId);
            var rowsDeleted = await connection.ExecuteAsync(sql, parameters);
            if (rowsDeleted <= 0)
            {
                return 0;
            }

            return rowsDeleted;
        }

        private async Task<int> DeleteCaparrotsIdentificatsPerGameAsync(int gameId)
        {
            var sql = "DELETE FROM CaparrotsIdentificatsPerGame WHERE IdGame=@IdGame";
            using var connection = _dataContext.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("IdGame", gameId);
            var rowsDeleted = await connection.ExecuteAsync(sql, parameters);
            if (rowsDeleted <= 0)
            {
                return 0;
            }
            return rowsDeleted;
        }
    }
}
