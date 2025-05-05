Geo Tracker🗺 - ASP.NET Core Web App

Geo Tracker is a secure and interactive ASP.NET Core 8.0 web application featuring user authentication, 
session management, Google Maps integration, and email notifications. It follows clean architecture and uses Entity Framework Core with the Code First approach.

## �� Project Structure

```
GeoTracker/
├── Controllers/               # Handles user and map-related requests
│   └── AccountController.cs
│   └── MapController.cs
├── Models/                    # Data models and ViewModels
│   └── User.cs
│   └── LoginViewModel.cs
│   └── RegisterViewModel.cs
│   └── ChangePasswordViewModel.cs
├── Views/
│   ├── Account/
│   │   └── Login.cshtml
│   │   └── Register.cshtml
│   │   └── ChangePassword.cshtml
│   ├── Map/
│   │   └── Index.cshtml
│   └── Shared/
│       └── _Layout.cshtml
│       └── _ValidationScriptsPartial.cshtml
├── Services/
│   └── EmailService.cs        # Handles sending emails using SMTP
├── ViewMessageBuilder.cs      # Centralized message constants
├── Program.cs                 # Application configuration
├── appsettings.json           # Configuration for email, DB, etc.
```

---

## ✨ Features

- ✅ User Registration with validation
- ✅ Login with Bootstrap error modal
- ✅ Change Password with session validation
- ✅ Google Maps integration with latitude/longitude input
- ✅ Email Notifications using SMTP (Gmail)
- ✅ Session Management without using magic strings
- ✅ Clean, modern UI with Bootstrap 5
- ✅ Centralized string messages with `ViewMessageBuilder`

---

## �� Authentication & Session

User's email is securely stored using `HttpContext.Session`. All authenticated routes validate the session before proceeding using a custom extension method.

```csharp
if (!HttpContext.UserIsAuthenticated()) return this.RedirectToLogin();
```

---

## ✉️ Email Configuration

Update your `appsettings.json` file:

```json
"EmailSettings": {
  "FromAddress": "your-email@gmail.com",
  "Username": "your-email@gmail.com",
  "Password": "your-app-password"
}
```

Inject `EmailService` where needed to send messages.

---

---

## ��️ Map Feature

- Accepts user-entered **latitude** and **longitude**
- Displays a **marker** using Google Maps
- Includes **client-side validation** for numeric inputs

---

## �� Validations

- ✅ JavaScript validation for form fields (age, name, etc.)
- ✅ Razor Data Annotations for server-side validation
- ✅ Age must be between 18 and 100

---

## ��️ Technologies Used

- ASP.NET Core 8.0 (Razor Pages + MVC)
- Entity Framework Core (Code First)
- SQL Server (can be configured)
- Google Maps API
- Bootstrap 5
- JavaScript (for client-side validation)

---

## �� Getting Started

1. **Clone the repo:**
   ```bash
   git clone https://github.com/your-username/GeoTracker.git
   ```

2. **Update `appsettings.json`:**
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=MapTaskDB;Trusted_Connection=True;"
   }
   ```

3. **Apply EF Core Migrations:**
   ```bash
   Add-Migration InitialCreate
   Update-Database
   ```

4. **Run the App:**
   ```bash
   dotnet run
   ```

---
   
## �� License

This project is licensed under the [MIT License](LICENSE).

---
## �� Output 

Register Page:
![image](https://github.com/user-attachments/assets/5aaec0ff-7ac2-4dc1-bed2-b03bf2eaa53b)

Login Page:
![image](https://github.com/user-attachments/assets/cc213f46-7487-4480-a4e9-444c1a8e1c0f)

ChangePassword Page:
![image](https://github.com/user-attachments/assets/33bea598-cb27-4b93-8bec-b97e22dd1779)

Map page:
![image](https://github.com/user-attachments/assets/c06672dd-2fef-4ed7-8513-1daa5741ae8c)








