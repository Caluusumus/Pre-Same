using ClassModels;

namespace MuseoCivicoAurora.Service
{
    public interface IOpereService
    {
        Task AddOperaAsync(Opera opera);
        Task DeleteOperaByIdAsync(Guid id);
        Task<Opera?> GetOperaByIdAsync(Guid id);
        Task<IEnumerable<Opera>> GetOpereAsync();
        Task UpdateOperaAsync(Opera opera);
    }
}