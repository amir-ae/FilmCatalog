namespace FilmCatalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public int PageSize = 5;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("/")]
        [Route("Home")]
        [Route("Home/Index/{id?}")]
        [Route("Page{page:int:min(1)}")]
        [Route("{genre:minlength(1)}/Page{page:int:min(1)}")]
        public ViewResult Index(string? genre, int page = 1)
        {
            Console.WriteLine(genre);
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = _context.Films?
                    .Where(f => genre == null || (f.Genre != null && f.Genre.Contains(genre)))
                    .Count() ?? 0
            };

            if (page > pagingInfo.TotalPages)
            {
                page = pagingInfo.TotalPages;
                pagingInfo.CurrentPage = page;
            }

            PostsListViewModel model = new PostsListViewModel
            {
                Films = _context.Films?
                    .OrderBy(f => f.Registration)
                    .Where(f => genre == null || (f.Genre != null && f.Genre.Contains(genre)))
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = pagingInfo,

                CurrentGenre = genre
            };

            return View(model);
        }

        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
