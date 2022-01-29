namespace FilmCatalog.Infrastructure
{
    public class FilmPosterAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Film? film = value as Film;
            if (film == null || film.PosterFile == null && string.IsNullOrEmpty(film.Poster))
            {
                return new ValidationResult(ErrorMessage ?? $"Error saving new poster");
            }
            return ValidationResult.Success;
        }
    }
}
