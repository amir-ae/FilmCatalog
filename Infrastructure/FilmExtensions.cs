namespace FilmCatalog.Infrastructure
{
    public static class FilmExtensions
    {
        public static string GetDisplayName(this Film film)
            => film.Title ?? string.Empty;
    }
}
