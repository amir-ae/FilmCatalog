namespace FilmCatalog.Pages
{
    public class UserPasswordRecoveryConfirmModel : UserPageModel
    {
        public UserPasswordRecoveryConfirmModel(UserManager<IdentityUser> usrMgr,
            TokenUrlEncoderService tokenUrlEncoder)
        {
            UserManager = usrMgr;
            TokenUrlEncoder = tokenUrlEncoder;
        }

        public UserManager<IdentityUser> UserManager { get; set; }

        public TokenUrlEncoderService TokenUrlEncoder { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Email { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Token { get; set; }

        [BindProperty]
        [Required]
        public string? Password { get; set; }

        [BindProperty]
        [Required]
        [Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid && Token != null)
            {
                string decodedToken = TokenUrlEncoder.DecodeToken(Token);
                IdentityUser user = await UserManager.FindByEmailAsync(Email);
                IdentityResult result = await UserManager.ResetPasswordAsync(user,
                    decodedToken, Password);
                if (result.Process(ModelState))
                {
                    TempData["message"] = "Password changed";
                    return RedirectToPage();
                }
            }
            return Page();
        }
    }
}
