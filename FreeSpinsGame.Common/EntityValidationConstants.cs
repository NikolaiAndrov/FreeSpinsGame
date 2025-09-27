namespace FreeSpinsGame.Common
{
    public static class EntityValidationConstants
    {
        public static class CampaignValidation
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;

            public const int SpinsPerDayMinValue = 1;
            public const int SpinsPerDayMaxValue = 1000;
        }

        public static class RegisterPlayerDtoValidation
        {
            public const int UserNameMinLength = 2;
            public const int UserNameMaxLength = 50;

            public const int PasswordMinLength = 3;
            public const int PasswordMaxLength = 50;
        }

        public static class CampaignQueryModelValidation
        {
            public const int MinItemsPerPage = 1;
            public const int MaxItemsPerPage = 50;

            public const int CurrentPageMinValue = 1;
            public const int CurrentPageMaxValue = 1000;
        }
    }
}
