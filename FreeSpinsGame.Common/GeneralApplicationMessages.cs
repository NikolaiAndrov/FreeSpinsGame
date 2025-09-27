namespace FreeSpinsGame.Common
{
    public static class GeneralApplicationMessages
    {
        // Log Messages
        public const string SuccessfulSpin = "Successfuly spined.";
        public const string ConcurrencyConflict = "Concurrency conflict occurred.";
        public const string StatusProvidedSuccessfuly = "Status provided successfuly.";
        public const string GetAllCampaignsSuccessfully = "All campaigns provided with success.";
        public const string GetAllCampaignsCritical = "While attempting to get all campaigns an error occurred.";

        public const string UnexpectedErrorMessage = "Unexpected error occurred, please try later or contact administrator.";
        public const string UserRegisteredSuccessfully = "The registration was successful.";

        public const string InvalidEmailOrPassword = "The email or password not correct.";

        public const string PlayerNotFound = "The player with these credentials is not existing.";
        public const string PlayerNotSubscribed = "You don't have subscription for this campaign.";
        public const string CampaignNotFound = "The campaign is no longer available.";
        public const string AllSpinsExhausted = "You already have exhausted all of your daily spin attempts.";
        public const string RemainingSpinsCount = "Your remaining daily spins count";
        public const string SpinConflict = "Something went wrong with your request, please retry.";

    }
}
