namespace FilmCatalog.Infrastructure
{
    public static class ContextExtensions
    {
        public static async Task CreatePost(this IdentityDbContext<IdentityUser> Context, Film Film)
        {
            await Context.AddAsync(Film);
            await Context.SaveChangesAsync();
        }

        public static async Task UpdatePost(this IdentityDbContext<IdentityUser> Context, Film Film)
        {
            Context.Update(Film);
            await Context.SaveChangesAsync();
        }

        public static async Task DeletePost(this IdentityDbContext<IdentityUser> Context, Film Film)
        {
            Context.Remove(Film);
            await Context.SaveChangesAsync();
        }
    }
}
