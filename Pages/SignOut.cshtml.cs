namespace FilmCatalog.Pages
{
    [AllowAnonymous]
    public class SignOutModel : UserPageModel
    {
        public SignOutModel(SignInManager<IdentityUser> signMgr)
            => SignInManager = signMgr;
        
        public SignInManager<IdentityUser> SignInManager { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
            await SignInManager.SignOutAsync();
            return RedirectToPage();
        }
    }
}
