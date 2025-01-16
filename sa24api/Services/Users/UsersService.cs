using Dapper;
using sa24api.Data;
using sa24api.Models;
using System.Data;

namespace sa24api.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly DataContext _dataContext;

        public UsersService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Usuari> InsertUserAsync(Usuari user)
        {
            var query = "INSERT INTO dbo.Usuaris (Nom, Email, PasswordHash, Instructor) VALUES (@Nom, @Email, @PasswordHash, @Instructor)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("Nom", user.Nom, DbType.String);
            parameters.Add("Email", user.Email, DbType.String);
            //byte[] hashBytes = System.Text.Encoding.Latin1.GetBytes(user.PasswordHash);
            //parameters.Add("PasswordHash", hashBytes, DbType.Binary);
            parameters.Add("PasswordHash", user.PasswordHash, DbType.String);
            parameters.Add("Instructor", user.Instructor, DbType.Boolean);

            using var connection = _dataContext.CreateConnection();

            var id = await connection.QuerySingleAsync<int>(query, parameters);

            var createdUser = new Usuari
            {
                Id = id,
                Nom = user.Nom,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Instructor = user.Instructor,
            };

            return createdUser;
        }

        public async Task<Usuari?> GetUserAsync(string userEmail)
        {
            var sql = "select Id,Nom,Email,PasswordHash,Foto,Instructor from dbo.Usuaris where Email=@Email";
            var parameters = new DynamicParameters();
            parameters.Add("Email", userEmail);
            using var conn = _dataContext.CreateConnection();
            try
            {
                var usuari = await conn.QueryFirstAsync<Usuari>(sql, parameters);
                return usuari;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
