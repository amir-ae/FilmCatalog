using Microsoft.Extensions.Primitives;

namespace FilmCatalog.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public NavigationMenuViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            List<string> genres = new List<string>();
            if (_context.Films?.Count() > 0)
            {
                foreach (string? genreData in _context.Films.Select(x => x.Genre))
                {
                    if (!string.IsNullOrEmpty(genreData))
                    {
                        foreach (string genre in genreData
                            .Split(new string[] { ",", ".", " - ", "/", "|" }, StringSplitOptions.None)
                            .Select(g => g.Trim()))
                        {
                            if (!genres.Contains(genre))
                            {
                                genres.Add(genre);
                            }
                        }
                    }
                }
            }

            return View(new NavigationMenuViewModel
            {
                Genres = genres.OrderBy(x => x),
                SelectedGenre = RouteData.Values["genre"] as string ?? HttpContext.Request.Query["genre"]
            });
        }
    }
}
