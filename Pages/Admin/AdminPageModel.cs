namespace FilmCatalog.Pages.Admin
{
    //[AllowAnonymous]
    [Authorize(Roles = "Dashboard")]
    public class AdminPageModel : UserPageModel
    {
        // no methods or properties required
    }
}
