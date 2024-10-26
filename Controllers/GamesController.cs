using Microsoft.AspNetCore.Http.HttpResults;

namespace NGINX.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _Context;
        private readonly ICategoriesServices _CategoriesServices;
        private readonly IDevicesServices _DevicesServices;
        private readonly IGamesServices _gamesServices;
        public GamesController(ApplicationDbContext context, ICategoriesServices categoriesServices, IDevicesServices devicesServices, IGamesServices gamesServices)
        {
            _Context = context;
            _CategoriesServices = categoriesServices;
            _DevicesServices = devicesServices;
            _gamesServices = gamesServices;
        }

        public IActionResult Index()
        {
            var Games = _gamesServices.GetAll();
            return View(Games);
        }
        public IActionResult Details(int id)
        {
            var game = _gamesServices.GetGame(id);
            if (game is null)
                return NotFound();
            else
                return View(game);


        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gamesServices.GetGame(id);
            if (game is null)
                return NotFound();
            EditGameFormViewModel viewmodel = new()
            {
                Id = id,
                Description = game.Description,
                Name = game.Name,
                CurrentCover = game.Cover,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _CategoriesServices.GetSelectList(),
                Devices = _DevicesServices.GetSelectList()
            };
            return View(viewmodel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = _CategoriesServices.GetSelectList(),
                Devices = _DevicesServices.GetSelectList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _CategoriesServices.GetSelectList();
                model.Devices = _DevicesServices.GetSelectList();
                return View(model);
            }

            await _gamesServices.Create(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.Categories = _CategoriesServices.GetSelectList();
                model.Devices = _DevicesServices.GetSelectList();
                return View(model);
            }

            var game = await _gamesServices.Edit(model);
            if (game is null)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var isDeleted = _gamesServices.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }

    }
}
