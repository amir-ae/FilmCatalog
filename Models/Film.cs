namespace FilmCatalog.Models
{
    [FilmPoster(ErrorMessage = "Пожалуйста, загрузите постер фильма")]
    public class Film
    {
        [Key]
        public long FilmId { get; set; }

        [MaxLength(100)]
        [Display(Name = "Название*")]
        [Required(ErrorMessage = "Пожалуйста, введите название фильма")]
        public string? Title { get; set; }

        [MaxLength(1200)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [MaxLength(100)]
        [Display(Name = "Режиссёр")]
        public string? Director { get; set; }

        [Display(Name = "Год выпуска")]
        public short? Year { get; set; }

        [MaxLength(100)]
        [Display(Name = "Жанр")]
        public string? Genre { get; set; }

        [MaxLength(100)]
        public string? Poster { get; set; }

        [NotMapped]
        [Display(Name = "Постер*")]
        public IFormFile? PosterFile { get; set; }

        [Column(TypeName = "DateTime")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm:ss tt}")]
        [Display(Name = "Дата регистрации")]
        public DateTime Registration { get; set; }

        [MaxLength(450)]
        public string? UserId { get; set; }
    }
}
