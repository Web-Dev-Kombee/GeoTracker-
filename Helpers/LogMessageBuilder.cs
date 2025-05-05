namespace MapTask.Helpers
{
    public static class LogMessageBuilder
    {
        public static string EmailSendFailure(string userEmail)
        {
            return $"❌ Failed to send email to {userEmail}";
        }

        public static string AdminNotificationFailure(string userEmail)
        {
            return $"⚠️ Failed to notify admin about the email failure to {userEmail}";
        }
    }
}
