namespace FilmCatalog.Services
{
    public class FilmPosterService
    {
        public FilmPosterService(IWebHostEnvironment env,
            IConfiguration config)
        {
            ImagesPath = string.Concat(env.WebRootPath, 
                config["Paths:Images"] ?? "/images");
        }

        public string ImagesPath { get; set; }

        public async Task<string?> SavePoster(Film film)
        {
            IFormFile? poster = film.PosterFile;
            if (poster != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(poster.FileName)
                    + DateTime.Now.ToString("yymmssfff")
                    + Path.GetExtension(poster.FileName);
                string path = Path.Combine(ImagesPath, fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await poster.CopyToAsync(fileStream);
                }
                return fileName;
            }
            return null;
        }

        public void DeletePoster(Film film)
        {
            string? fileName = film.Poster;
            if (!string.IsNullOrEmpty(fileName))
            {
                string path = Path.Combine(ImagesPath, fileName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
    }
}
