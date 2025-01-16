using sa24api.Models;

namespace sa24api.Services.PartidasLevel2
{
    public interface IPartidasLevel2Service
    {
        public Task<Partida> InsertPartidaAsync(int idUser, int idPartida);
        public Task<int> UpdatePuntuacionPartidaAsync(Partida partida);
        public Task<Partida> GetPartidaAsync(int idPartida);
        public Task<IEnumerable<Partida>> GetClassificationLevel2();
        public Task<int> DeletePartidaLevel2Async(int idPartida);
    }
}
