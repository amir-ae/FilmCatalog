namespace FilmCatalog.Models.ViewModels
{
    public class NavigationMenuViewModel
    {
        public IEnumerable<string>? Genres { get; set; }
        public string? SelectedGenre { get; set; }
    }
}