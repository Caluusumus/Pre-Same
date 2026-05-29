using ClassModels;

namespace MuseoCivicoAurora.Service
{
    public interface IPrenotazioniService
    {
        Task AddPrenotazioneAsync(Prenotazione Prenotazione);
        Task DeletePrenotazioneByIdAsync(Guid id);
        Task<Prenotazione?> GetPrenotazioneByIdAsync(Guid id);
        Task<IEnumerable<Prenotazione>> GetPrenotazioniAsync();
        Task UpdatePrenotazioneAsync(Prenotazione Prenotazione);
    }
}