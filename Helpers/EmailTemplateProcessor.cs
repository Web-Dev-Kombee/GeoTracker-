namespace MapTask.Helpers
{
    public static class EmailTemplateProcessor
    {
        public static string BuildWelcomeEmail(string templateHtml, string fullName, string email, string password)
        {
            return templateHtml
                .Replace("{{FullName}}", fullName)
                .Replace("{{Email}}", email)
                .Replace("{{Password}}", password);
        }
        public static string BuildPasswordChangeEmail(string template, string name, string email)
        {
            return template.Replace("{{UserName}}", name)
                           .Replace("{{UserEmail}}", email);
        }
    }
}