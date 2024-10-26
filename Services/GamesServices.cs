
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Security.Cryptography.Xml;

namespace NGINX.Services
{
    public class GamesServices : IGamesServices
    {
        private readonly ApplicationDbContext _Context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _ImagesPath;

        public GamesServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _Context = context;
            _webHostEnvironment = webHostEnvironment;
            _ImagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

        public async Task Create(CreateGameFormViewModel model)
        {
            var coverName = await SaveCover(model.Cover);

            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
            };
            _Context.Add(game);
            _Context.SaveChanges();
        }
        public Game? GetGame(int id)
        {
            return _Context.Games
                  .Include(g => g.Category)
                  .Include(d => d.Devices)
                  .ThenInclude(d => d.Device)
                  .AsNoTracking()
                  .SingleOrDefault(g => g.Id == id);
        }
        public IEnumerable<Game> GetAll()
        {
            return _Context.Games
                .Include(g => g.Category)
                .Include(d => d.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .ToList();
        }

        public bool Delete(int id)
        {
            var isDeleted = false;
            var game = _Context.Games.Find(id);
            if (game is null)
                return isDeleted;

            _Context.Games.Remove(game);

            var effectedrows = _Context.SaveChanges();
            if (effectedrows > 0)
            {
                isDeleted = true;

                var cover = Path.Combine(_ImagesPath , game.Cover);
                File.Delete(cover);
            }
            return isDeleted;
        }

        public async Task<Game?> Edit(EditGameFormViewModel model)
        {
            var game = _Context.Games.Include(g => g.Devices).SingleOrDefault(d => d.Id == model.Id);
            if (game is null)
                return null;
            var hasnewCover = model.Cover is not null;
            var oldcover = game.Cover;
            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();
            if (hasnewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }
            var effectedrows = _Context.SaveChanges();

            if (effectedrows > 0)
            {
                if (hasnewCover)
                {
                    var cover = Path.Combine(_ImagesPath, oldcover );
                    File.Delete(cover);
                }
            }
            else
            {
                var cover = Path.Combine(_ImagesPath, game.Cover);
                File.Delete(cover);

                return null;
            }
            return game;
        }
        private async Task<string> SaveCover(IFormFile Cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}";
            var path = Path.Combine(_ImagesPath, coverName);

            using var Stream = File.Create(path);
            await Cover.CopyToAsync(Stream);
            return coverName;
        }
    }
}
