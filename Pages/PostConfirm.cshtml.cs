namespace FilmCatalog.Pages
{
    public class PostConfirmModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? Task { get; set; } = "Create";

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; } = "ListPosts";

        [BindProperty]
        public string Message { get; set; } = "Post successfully ";

        public void OnGet()
        {
            SetProperties();
        }

        private void SetProperties()
        {
            switch (Task?.ToLowerInvariant())
            {
                case "create":
                    Message += " created!";
                    break;
                case "edit":
                    Message += " edited!";
                    break;
                case "delete":
                    Message += " deleted!";
                    break;
                default:
                    Message += " updated!";
                    break;
            }
        }
    }
}
