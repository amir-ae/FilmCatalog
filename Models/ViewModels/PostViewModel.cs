namespace FilmCatalog.Models.ViewModels
{
    public class PostViewModel
    {
        public string Action { get; set; } = "Create";
        public string ActionRu { get; set; } = "Создавать";
        public bool ReadOnly { get; set; } = false;
        public string Theme { get; set; } = "primary";
        public bool ShowPoster { get; set; } = true;
        public bool ShowPosterInput { get; set; } = false;
        public bool ChangePoster { get; set; } = false;
        public bool ShowRegistration { get; set; } = true;
        public bool ShowAction { get; set; } = true;
        public bool ShowBack { get; set; } = true;
    }
}
