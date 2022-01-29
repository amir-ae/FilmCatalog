namespace FilmCatalog.Pages.Admin
{
    public class CreateModel : AdminPageModel
    {
        public CreateModel(UserManager<IdentityUser> mgr,
            IdentityEmailService emailService)
        {
            UserManager = mgr;
            EmailService = emailService;
        }

        public UserManager<IdentityUser> UserManager { get; set; }

        public IdentityEmailService EmailService { get; set; }

        [BindProperty(SupportsGet = true)]
        [EmailAddress]
        public string? Email { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = Email,
                    Email = Email,
                    EmailConfirmed = true
                };
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Process(ModelState))
                {
                    await EmailService.SendPasswordRecoveryEmail(user,
                        "/Identity/UserAccountComplete");
                    TempData["message"] = "Account Created";
                    return RedirectToPage();
                }
            }
            return Page();
        }
    }
}
