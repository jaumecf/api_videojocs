using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sa24api.Data;
using sa24api.Models;
using System.Data;

namespace sa24api.Services.PartidasLevel2
{
    public class PartidasLevel2Service : IPartidasLevel2Service
    {
        private readonly DataContext _dataContext;

        public PartidasLevel2Service(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Partida> InsertPartidaAsync(int idUser, int idPartida)
        {
            var query = "INSERT INTO dbo.Partidas (Id, UserId, Puntuacio_Final) VALUES (@Id, @userId, @Puntuacio_Final)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", idPartida);
            parameters.Add("UserId", idUser, DbType.Int32);
            parameters.Add("Puntuacio_Final", -1);
            using var connection = _dataContext.CreateConnection();

            var rowsAffected = await connection.ExecuteAsync(query, parameters);

            if (rowsAffected != 1)
            {
                return null;
            }

            //int idPartida = await GetLastInsertedPartidaId();
            Partida createdPartida = new Partida();
            createdPartida.IdPartida = idPartida;
            createdPartida.UserId = idUser;
            createdPartida.Puntuacio_Final = -1;

            return createdPartida;
        }

        public async Task<int> UpdatePuntuacionPartidaAsync(Partida partida)
        {
            var query = "UPDATE dbo.Partidas SET Puntuacio_Final=@Puntuacio_Final WHERE Id=@Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", partida.IdPartida);
            parameters.Add("Puntuacio_Final", partida.Puntuacio_Final);
            using var connection = _dataContext.CreateConnection();

            var rowsAffected = await connection.ExecuteAsync(query, parameters);

            return rowsAffected;
        }

        public async Task<Partida> GetPartidaAsync(int idPartida)
        {
            var query = "SELECT * FROM dbo.Partidas WHERE Partidas.Id=@IdPartida";

            using var connection = _dataContext.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("IdPartida", idPartida);
            var _partidas = await connection.QueryAsync<Partida>(query, parameters);

            if (_partidas != null && _partidas.ToList<Partida>().Count > 0)
            {
                return _partidas.First();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Partida>> GetClassificationLevel2()
        {
            String sql = "SELECT Partidas.Id AS IdPartida, Partidas.UserId, Usuaris.Nom AS UserName, Puntuacio_Final"
                + " FROM Partidas INNER JOIN Usuaris ON Partidas.UserId=Usuaris.Id"
                + " INNER JOIN (SELECT Partidas.UserId, MAX(Puntuacio_Final) AS MaxPuntuacio_Final"
                + " FROM Partidas"
                + " GROUP BY Partidas.UserId) A ON Partidas.UserId=A.UserId AND Partidas.Puntuacio_Final=A.MaxPuntuacio_Final"
                + " WHERE Partidas.UserId > 103"
                + " ORDER BY Partidas.Puntuacio_Final DESC";
            using var connection = _dataContext.CreateConnection();
            var partidas = await connection.QueryAsync<Partida>(sql);

            return partidas;
        }

        public async Task<int> DeletePartidaLevel2Async(int idPartida)
        {
            int eventsDeleted = await DeleteEventsDePartidaAsync(idPartida);

            if (eventsDeleted == 0)
            {
                // No havia events registrats per aquesta partida
            }
            else
            {
                // S'han eliminat x events
            }

            var sql = "DELETE FROM Partidas WHERE Id=@Id";
            using var connection = _dataContext.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("Id", idPartida);
            var rowsDeleted = await connection.ExecuteAsync(sql, parameters);
            if (rowsDeleted <= 0)
            {
                return 0;
            }

            return rowsDeleted;
        }

        private async Task<int> DeleteEventsDePartidaAsync(int idPartida)
        {
            var sql = "DELETE FROM Events WHERE idPartida=@idPartida";
            using var connection = _dataContext.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("idPartida", idPartida);
            var rowsDeleted = await connection.ExecuteAsync(sql, parameters);
            if (rowsDeleted <= 0)
            {
                return 0;
            }
            return rowsDeleted;
        }
    }
}
