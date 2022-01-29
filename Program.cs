var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDataConnection"));
});

builder.Services.AddScoped<IEmailSender, ConsoleEmailSender>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts => {
    opts.Password.RequiredLength = 8;
    opts.Password.RequireDigit = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<TokenUrlEncoderService>();
builder.Services.AddScoped<IdentityEmailService>();
builder.Services.AddScoped<FilmPosterService>();

builder.Services.ConfigureApplicationCookie(opts => {
    opts.LoginPath = "/SignIn";
    opts.LogoutPath = "/SignOut";
    opts.AccessDeniedPath = "/Forbidden";
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options => {
    options.Cookie.IsEssential = true;
});

builder.Services.Configure<SecurityStampValidatorOptions>(opts => {
    opts.ValidationInterval = System.TimeSpan.FromMinutes(1);
});

builder.Services.AddHttpsRedirection(opts => {
    opts.HttpsPort = 44350;
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseExceptionHandler("/Error");
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.SeedUserStoreForDashboard();

app.Run();

