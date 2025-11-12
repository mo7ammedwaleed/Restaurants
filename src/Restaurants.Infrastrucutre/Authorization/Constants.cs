namespace Restaurants.Infrastrucutre.Authorization
{
    public static class PolicyNames
    {
        public const string HasNationality = "HasNationality";
        public const string AtLeast20 = "AtLeast20";
        public const string CreatedOfAtLeast2Restaurants = "CreatedOfAtLeast2Restaurants";
    }

    public static class AppClaimsTypes
    {
        public const string Nationality = "Nationality";
        public const string DateOfBirth = "DateOfBirth";
    }
}
