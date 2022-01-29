namespace FilmCatalog.Models.ViewModels
{
    public class PostsListViewModel
    {
        public IEnumerable<Film>? Films { get; set; }
        public PagingInfo? PagingInfo { get; set; }
        public string? CurrentGenre { get; set; }
    }
}
