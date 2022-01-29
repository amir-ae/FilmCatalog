namespace FilmCatalog.Pages
{
    public class PostModel : PageModel
    {
        public PostModel(ApplicationDbContext context,
            FilmPosterService posterService)
        {
            Context = context;
            PosterService = posterService;
        }
        public ApplicationDbContext Context { get; set; }
        public FilmPosterService PosterService { get; set; }

        [BindProperty(SupportsGet = true)]
        public long? Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Task { get; set; } = "Create";

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; } = "ListPosts";

        public Film Film { get; set; } = new Film();

        public PostViewModel ViewModel { get; set; } = PostViewModelFactory.Create();

        public string? Label { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id != null && Context?.Films != null)
            {
                Film? film = await Context.Films.FirstOrDefaultAsync(f => f.FilmId == Id);
                if (film != null)
                {
                    Film = film;
                }
            }
            SetProperties();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Film film)
        {
            if (ModelState.IsValid)
            {
                film.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                film.Registration = DateTime.Now;
                if (film.FilmId == 0 || Task?.ToLowerInvariant() == "create")
                {
                    film.Poster = await PosterService.SavePoster(film);
                    await Context.CreatePost(film);
                }
                else if (film.FilmId > 0 || Task?.ToLowerInvariant() == "edit")
                {
                    if (film.PosterFile != null)
                    {
                        PosterService.DeletePoster(film);
                        film.Poster = await PosterService.SavePoster(film);
                    }
                    await Context.UpdatePost(film);
                }
                return RedirectToPage("PostConfirm", new { Task = Task, ReturnUrl = ReturnUrl });
            }
            SetProperties();
            return Page();
        }

        private void SetProperties()
        {
            switch (Task?.ToLowerInvariant())
            {
                case "view":
                    ViewModel = PostViewModelFactory.View();
                    Label = "ViewPost";
                    break;
                case "delete":
                    ViewModel = PostViewModelFactory.Delete();
                    Label = "DeletePost";
                    break;
                case "edit":
                    ViewModel = PostViewModelFactory.Edit();
                    Label = "EditPost";
                    break;
                case "create":
                default:
                    ViewModel = PostViewModelFactory.Create();
                    Label = "CreatePost";
                    break;
            }
        }
    }
}
