namespace FilmCatalog.Models
{
    public static class ApplicationClaimTypes
    {
        public const string Name = ClaimTypes.Name;

        public const string Surname = ClaimTypes.Surname;

        public const string Country = ClaimTypes.Country;

        public static string GetDisplayName(this Claim claim)
            => GetDisplayName(claim.Type);

        public static string GetDisplayName(string claimType)
            => typeof(ClaimTypes).GetFields().Where(field =>
                field.GetRawConstantValue()?.ToString() == claimType)
                    .Select(field => field.Name)
                    .FirstOrDefault() ?? claimType;

        public static IEnumerable<(string type, string display)> AppClaimTypes
            = new[] { Name, Surname, Country }.Select(c =>
                (c, GetDisplayName(c)));
    }
}
