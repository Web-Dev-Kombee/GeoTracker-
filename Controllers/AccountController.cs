using MapTask.Data;
using MapTask.Helpers;
using MapTask.Models;
using MapTask.Services;
using MapTask.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MapTask.Controllers
{
    public class AccountController : BaseController
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;
        private readonly ILogger<AccountController> _logger;
        private readonly PasswordSettings _passwordSettings;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AccountController(AppDbContext context, EmailService emailService, ILogger<AccountController> logger, IOptions<PasswordSettings> passwordSettings, IConfiguration configuration, IUserService userService)
        {
            _context = context;
            _emailService = emailService;
            _logger = logger;
            _passwordSettings = passwordSettings.Value;
            _configuration = configuration;
            _userService = userService;
        }

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (_userService.IsEmailTaken(user.Email))
                {
                    ViewBag.Message = ViewMessageBuilder.EmailAlreadyInUse;
                    return View(user);
                }
                var randomPassword = GenerateRandomPassword();
                user.Password = PasswordHasher.HashPassword(randomPassword);

                _userService.RegisterUser(user);

                try
                {
                    string templatePath = Path.Combine(Directory.GetCurrentDirectory(), EmailTemplatePaths.WelcomeEmailRelativePath);
                    string templateHtml = System.IO.File.ReadAllText(templatePath);
                    string fullName = $"{user.FirstName} {user.LastName}";
                    string htmlMessage = EmailTemplateProcessor.BuildWelcomeEmail(templateHtml, fullName, user.Email, randomPassword);
                    _emailService.SendEmail(user.Email, EmailMessageBuilder.WelcomeEmailSubject, htmlMessage);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, LogMessageBuilder.EmailSendFailure(user.Email));
                    string adminEmail = _configuration["AdminSettings:Email"];
                    string adminMessage = EmailMessageBuilder.BuildAdminFailureMessage(user.Email, ex.Message);

                    try
                    {
                        _emailService.SendEmail(adminEmail, EmailMessageBuilder.AdminFailureEmailSubject, adminMessage);
                    }
                    catch (Exception innerEx)
                    {
                        _logger.LogWarning(innerEx, LogMessageBuilder.AdminNotificationFailure(user.Email));
                    }
                }

                return RedirectToLogin();
            }
            return View(user);
        }

        public IActionResult Login() => View();


        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var hashedPassword = PasswordHasher.HashPassword(password);
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == hashedPassword);
            if (user != null)
            {
                HttpContext.Session.SetString(SessionKeys.UserEmail, user.Email);
                return RedirectToMap();
            }
            ViewBag.ShowErrorModal = true;
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToLogin();
        }


        private string GenerateRandomPassword()
        {
            var chars = _passwordSettings.AllowedChars;
            var length = _passwordSettings.Length;
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeys.UserEmail)))
            {
                return RedirectToLogin();
            }
            return View();
        }


        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userEmail = HttpContext.Session.GetString(SessionKeys.UserEmail);
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
            {
                ViewBag.Message = ViewMessageBuilder.IncorrectCurrentPassword;
                return View(model);
            }
            // Hash the entered current password to compare
            var hashedCurrent = PasswordHasher.HashPassword(model.CurrentPassword);
            if (user.Password != hashedCurrent)
            {
                ViewBag.Message = ViewMessageBuilder.IncorrectCurrentPassword;
                return View(model);
            }
            // Hash and update the new password
            user.Password = PasswordHasher.HashPassword(model.NewPassword);
            _context.SaveChanges();
            //ViewBag.Message = ViewMessageBuilder.PasswordUpdateSuccess;
            //return RedirectToLogin();

            try
            {
                string templatePath = Path.Combine(Directory.GetCurrentDirectory(), EmailTemplatePaths.PasswordChangeRelativePath);
                string templateHtml = System.IO.File.ReadAllText(templatePath);
                string fullName = $"{user.FirstName} {user.LastName}";
                string htmlMessage = EmailTemplateProcessor.BuildPasswordChangeEmail(templateHtml, fullName, user.Email);
                _emailService.SendEmail(user.Email, EmailMessageBuilder.PasswordChangeSubject, htmlMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send password change confirmation to {user.Email}");
            }
            ViewBag.Message = ViewMessageBuilder.PasswordUpdateSuccess;
            return RedirectToLogin();
        }

    }

    }




