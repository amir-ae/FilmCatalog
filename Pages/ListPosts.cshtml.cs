namespace FilmCatalog.Pages
{
    public class ListPostsModel : UserPageModel
    {
        public ListPostsModel(ApplicationDbContext context)
        {
            Context = context;
        }
        public ApplicationDbContext Context { get; set; }
        public IEnumerable<Film>? Films { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Filter { get; set; }

        public void OnGet()
        {
            string? userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                Films = Context.Films?
                    .Where(f => f.UserId == userId)
                    .Where(f => Filter == null || f.Title != null && f.Title.Contains(Filter))
                    .OrderBy(f => f.Title).ToList();
            }
        }

        public IActionResult OnPostFilter() => RedirectToPage(new { Filter });

        public async Task<IActionResult> OnPostAsync([Required] long id, [Required] string task)
        {
            if (Context.Films != null)
            {
                Film? film = await Context.Films.FindAsync(id);
                if (film != null)
                {
                    switch (task.ToLowerInvariant())
                    {
                        case "view":
                        case "edit":
                            object routeValues = new { Id = id, Task = task, ReturnUrl = HttpContext?.Request.Path };
                            return RedirectToPage("Post", routeValues);
                        case "delete":
                            await Context.DeletePost(film);
                            return RedirectToPage();
                    };
                }
            }
            return Page();
        }
    }
}
