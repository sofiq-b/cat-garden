namespace CatGarden.Common
{
    public static class GeneralApplicationConstants
    {
        public const int ReleaseYear = 2024;

        public const string DateFormat = "dd/MM/yyyy";

        public const string CatAttributeRequired = "The cat's {0} is required.";

        public const int DefaultPage = 1;
        public const int EntitiesPerPage = 4;

        public const string AdminRoleName = "Administrator";
        public const string DevelopmentAdminEmail = "admin@catgarden.bg";
        public const string AdminAreaName = "Admin";

        public const string UsersCacheKey = "UsersCache";
        public const int UsersCacheDurationMinutes = 5;
    }
}
