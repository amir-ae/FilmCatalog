namespace FilmCatalog.Pages.Admin
{
    public class FeaturesModel : AdminPageModel
    {
        public FeaturesModel(UserManager<IdentityUser> mgr)
            => UserManager = mgr;

        public UserManager<IdentityUser> UserManager { get; set; }

        public IEnumerable<(string, string?)>? Features { get; set; }

        public void OnGet()
        {
            Features = UserManager.GetType().GetProperties()
                .Where(prop => prop.Name.StartsWith("Supports"))
                .OrderBy(prop => prop.Name)
                .Select(prop => (prop.Name.ToString(), prop.GetValue(UserManager)?.ToString()));
        }
    }
}
