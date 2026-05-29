using ClassModels;

namespace MuseoCivicoAurora.Service
{
    public interface IBigliettiService
    {
        Task AddBigliettoAsync(Mostra mostra);
        Task DeleteBigliettoByIdAsync(Guid id);
        Task<IEnumerable<Mostra>> GetBigliettiAsync();
        Task<Mostra?> GetBigliettoByIdAsync(Guid id);
        Task UpdateBigliettoAsync(Mostra mostra);
    }
}