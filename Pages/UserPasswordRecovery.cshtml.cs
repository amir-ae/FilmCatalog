namespace FilmCatalog.Pages
{
    [AllowAnonymous]
    public class UserPasswordRecoveryModel : UserPageModel
    {
        public UserPasswordRecoveryModel(UserManager<IdentityUser> usrMgr,
            IdentityEmailService emailService)
        {
            UserManager = usrMgr;
            EmailService = emailService;
        }

        public UserManager<IdentityUser> UserManager { get; set; }
        public IdentityEmailService EmailService { get; set; }

        public async Task<IActionResult> OnPostAsync([Required] string email)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await UserManager.FindByEmailAsync(email);
                if (user != null)
                {
                    await EmailService.SendPasswordRecoveryEmail(user,
                        "UserPasswordRecoveryConfirm");
                }
                TempData["message"] = "We have sent you an email. "
                    + " Click the link it contains to choose a new password.";
                return RedirectToPage();
            }
            return Page();
        }
    }
}
