namespace NGINX.Services
{
    public interface IGamesServices
    {
        Task Create(CreateGameFormViewModel model);
        Game? GetGame(int id);
        Task<Game?> Edit(EditGameFormViewModel model);
        bool Delete(int id);
        IEnumerable<Game> GetAll();
    }
}
