using ClassModels;

namespace MuseoCivicoAurora.Service
{
    public interface IMostreService
    {
        Task AddMostraAsync(Mostra mostra);
        Task DeleteMostraByIdAsync(Guid id);
        Task<Mostra?> GetMostraByIdAsync(Guid id);
        Task<IEnumerable<Mostra>> GetMostreAsync();
        Task UpdateMostraAsync(Mostra mostra);
    }
}