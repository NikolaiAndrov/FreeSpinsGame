namespace FreeSpinsGame.Common
{
    public static class EntityValidationConstants
    {
        public static class CampaignValidation
        {
            public const int NameMaxLength = 50;
        }

        public static class RegisterPlayerDtoValidation
        {
            public const int UserNameMinLength = 2;
            public const int UserNameMaxLength = 50;

            public const int PasswordMinLength = 3;
            public const int PasswordMaxLength = 50;
        }
    }
}
