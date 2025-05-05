namespace MapTask.Helpers
{
    public static class EmailMessageBuilder
    {
        public static string BuildAdminFailureMessage(string userEmail, string errorMessage)
        {
            return $"""
                <p>Email to user <strong>{userEmail}</strong> failed.</p>
                <p>Error: {errorMessage}</p>
            """;
        }
        public static string WelcomeEmailSubject => "Welcome MapTask :tada:";
        public static string PasswordChangeSubject => "Your Password Has Been Changed";
        public static string AdminFailureEmailSubject => "MapTask Email Failure Notification :siren:";
    }
}