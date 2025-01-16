using Dapper;
using sa24api.Data;
using sa24api.Models;
using System.Data;

namespace sa24api.Services.Events
{
    public class EventsService : IEventsService
    {
        private readonly DataContext _dataContext;

        public EventsService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Event> InsertEventDimoniMatatAsync(Event _event)
        {
            var query = "INSERT INTO dbo.Events (idPartida, Pos_PlayerX, Pos_PlayerY, Pos_PlayerZ, Rot_PlayerX, Rot_PlayerY, Rot_PlayerZ, Time, EnemicX, EnemicY, EnemicZ, Event, Vida_Cupula, Num_Naus) VALUES " +
                "(@idPartida, @Pos_PlayerX, @Pos_PlayerY, @Pos_PlayerZ, @Rot_PlayerX, @Rot_PlayerY, @Rot_PlayerZ, GETDATE(), @EnemicX, @EnemicY, @EnemicZ, 'Dimoni RIP', @Vida_Cupula, @Num_Naus)";

            var parameters = new DynamicParameters();
            parameters.Add("idPartida", _event.idPartida, DbType.Int32);
            parameters.Add("Pos_PlayerX", _event.pos_PlayerX, DbType.Double);
            parameters.Add("Pos_PlayerY", _event.pos_PlayerY, DbType.Double);
            parameters.Add("Pos_PlayerZ", _event.pos_PlayerZ, DbType.Double);
            parameters.Add("Rot_PlayerX", _event.rot_PlayerX, DbType.Double);
            parameters.Add("Rot_PlayerY", _event.rot_PlayerY, DbType.Double);
            parameters.Add("Rot_PlayerZ", _event.rot_PlayerZ, DbType.Double);
            parameters.Add("EnemicX", _event.enemicX, DbType.Double);
            parameters.Add("EnemicY", _event.enemicY, DbType.Double);
            parameters.Add("EnemicZ", _event.enemicZ, DbType.Double);
            parameters.Add("Vida_Cupula", _event.vida_Cupula, DbType.Double);
            parameters.Add("Num_Naus", _event.num_Naus, DbType.Double);

            using var connection = _dataContext.CreateConnection();

            var rowsAffected = await connection.ExecuteAsync(query, parameters);

            if (rowsAffected != 1)
            {
                return null;
            }
            return _event;
        }

        public async Task<Event> InsertEventInvaderMatatAsync(Event _event)
        {
            var query = "INSERT INTO dbo.Events (idPartida, Pos_PlayerX, Pos_PlayerY, Pos_PlayerZ, Rot_PlayerX, Rot_PlayerY, Rot_PlayerZ, Time, EnemicX, EnemicY, EnemicZ, Event, Vida_Cupula, Num_Naus) VALUES " +
                "(@idPartida, @Pos_PlayerX, @Pos_PlayerY, @Pos_PlayerZ, @Rot_PlayerX, @Rot_PlayerY, @Rot_PlayerZ, GETDATE(), @EnemicX, @EnemicY, @EnemicZ, 'Invader RIP', @Vida_Cupula, @Num_Naus)";

            var parameters = new DynamicParameters();
            parameters.Add("idPartida", _event.idPartida, DbType.Int32);
            parameters.Add("Pos_PlayerX", _event.pos_PlayerX, DbType.Double);
            parameters.Add("Pos_PlayerY", _event.pos_PlayerY, DbType.Double);
            parameters.Add("Pos_PlayerZ", _event.pos_PlayerZ, DbType.Double);
            parameters.Add("Rot_PlayerX", _event.rot_PlayerX, DbType.Double);
            parameters.Add("Rot_PlayerY", _event.rot_PlayerY, DbType.Double);
            parameters.Add("Rot_PlayerZ", _event.rot_PlayerZ, DbType.Double);
            parameters.Add("EnemicX", _event.enemicX, DbType.Double);
            parameters.Add("EnemicY", _event.enemicY, DbType.Double);
            parameters.Add("EnemicZ", _event.enemicZ, DbType.Double);
            parameters.Add("Vida_Cupula", _event.vida_Cupula, DbType.Double);
            parameters.Add("Num_Naus", _event.num_Naus, DbType.Double);

            using var connection = _dataContext.CreateConnection();

            var rowsAffected = await connection.ExecuteAsync(query, parameters);

            if (rowsAffected != 1)
            {
                return null;
            }
            return _event;
        }

        public async Task<Event> InsertEventDimoniCreat(Event _event)
        {
            var query = "INSERT INTO dbo.Events (idPartida, Pos_PlayerX, Pos_PlayerY, Pos_PlayerZ, Rot_PlayerX, Rot_PlayerY, Rot_PlayerZ, Time, EnemicX, EnemicY, EnemicZ, Event, Vida_Cupula, Num_Naus) VALUES " +
                "(@idPartida, @Pos_PlayerX, @Pos_PlayerY, @Pos_PlayerZ, @Rot_PlayerX, @Rot_PlayerY, @Rot_PlayerZ, GETDATE(), @EnemicX, @EnemicY, @EnemicZ, 'Dimoni SPAWN', -1, -1)";

            var parameters = new DynamicParameters();
            parameters.Add("idPartida", _event.idPartida, DbType.Int32);
            parameters.Add("Pos_PlayerX", _event.pos_PlayerX, DbType.Double);
            parameters.Add("Pos_PlayerY", _event.pos_PlayerY, DbType.Double);
            parameters.Add("Pos_PlayerZ", _event.pos_PlayerZ, DbType.Double);
            parameters.Add("Rot_PlayerX", _event.rot_PlayerX, DbType.Double);
            parameters.Add("Rot_PlayerY", _event.rot_PlayerY, DbType.Double);
            parameters.Add("Rot_PlayerZ", _event.rot_PlayerZ, DbType.Double);
            parameters.Add("EnemicX", _event.enemicX, DbType.Double);
            parameters.Add("EnemicY", _event.enemicY, DbType.Double);
            parameters.Add("EnemicZ", _event.enemicZ, DbType.Double);

            using var connection = _dataContext.CreateConnection();

            var rowsAffected = await connection.ExecuteAsync(query, parameters);

            if (rowsAffected != 1)
            {
                return null;
            }
            return _event;
        }
    }
}
