<p align= "center">
    <img src="src\TopicTalks.Web\wwwroot\img\logo.svg" title="TopicTalks Logo" alt="TopicTalks Logo" width="500px">
    <br/>
    Talk about it. Learn about it.
</p>

## 🔴 Live Demo

- **Web:** https://projects.rawfin.net/TopicTalks
- **Api:** https://projects.rawfin.net/TopicTalks/api

## 📚 Table of Contents

- 💎 [Features](#-features)
- 🚀 [How to Run](#-how-to-run)
  - I. 📝 [Requirements](#i-requirements-)
  - II. ⚙️ [Installation](#ii-installation-%EF%B8%8F)
    - [Clone the Repository](#1-clone-the-repository)
    - [Database Migration](#2-database-migration)
    - [Configure Email Settings](#3-configure-email-settings-via-user-secrets-)
    - [Setup Google Cloud API Credentials (Optional)️](#4-setup-google-cloud-api-credentials-optional-%EF%B8%8F)
    - [Build the Projects](#5-build-the-projects)
    - [Run the Projects](#6-run-the-projects-seperately)
    - [Access the Projects](#7-access-the-projects-)
- 🛠️ [Technologies and Design Patterns](#%EF%B8%8F-technologies-and-design-patterns)
- 📦 [Nuget Packages](#-nuget-packages)
- 📊 [ER Diagram](#-er-diagram)
- 📸 [Screenshots](#-screenshots)
- 🪪 [License](#-license)

## ⭐ Give It a Star

If you find this project useful or interesting, please consider giving it a star. Thank you! 🤗

[![GitHub stars](https://img.shields.io/github/stars/Raofin/TopicTalks?style=social)](https://github.com/Raofin/TopicTalks/stargazers)

## 💎 Key Features

- **Threaded Q\&A**: Ask questions, get answers, and engage in deep, organized discussions.
- **Real-Time Alerts**: Stay updated with instant and email notifications on thread activity.
- **PDF Export**: Download full discussions with user and question details for offline access.
- **Excel Export**: Export user data in spreadsheet format.
- **Secure Access**: Role-based authorization ensure a safe, controlled environment.
- **Clean UI**: Modern, user-friendly design that’s intuitive and distraction-free.

## 🚀 How to Run

### I. Requirements 📝

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (or higher)
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads#:~:text=Download%20now-,Express,-SQL%20Server%202022)
- [JetBrains Rider](https://www.jetbrains.com/community/education/#students) (edu), [Visual Studio](https://visualstudio.microsoft.com/vs/community), or [VS Code](https://code.visualstudio.com)

### II. Installation ⚙️

#### 1. Clone the Repository

  ```powershell
  git clone https://github.com/Raofin/TopicTalks
  ```

#### 2. Database Migration

The project is configured to automatically apply migrations with some seed data on its **first run**. To create a database with dummy data, including the beautiful [user portraits](assets/portraits) and question covers, execute the [TopicTalks.sql](src/TopicTalks.Infrastructure/Persistence/DatabaseScripts/TopicTalks.sql) script. You can manually apply migrations using the following commands:

- For Package Manager 👇
  ```powershell
  Update-Database -Context AppDbContext -Project TopicTalks.Infrastructure -StartupProject TopicTalks.Api
  ```
- For CLI 👇
  ```powershell
  dotnet ef database update --project TopicTalks.Infrastructure/TopicTalks.Infrastructure.csproj --startup-project TopicTalks.Api/TopicTalks.Api.csproj
  ```

<details>
  <summary><b>🌻 Useful Commands</b></summary>

```powershell
Add-Migration Init -Context AppDbContext -Project TopicTalks.Infrastructure -StartupProject TopicTalks.Api
```

```powershell
Remove-Migration -Project TopicTalks.Infrastructure -StartupProject TopicTalks.Api -Force
```

```powershell
Update-Database -Context AppDbContext -Project TopicTalks.Infrastructure -StartupProject TopicTalks.Api
```

```powershell
Update-Database -Migration Init -Context AppDbContext -Project TopicTalks.Infrastructure -StartupProject TopicTalks.Api
```

</details>

#### 3. Configure Email Settings via User Secrets 📬

To use Gmail's smtp server, you will need to use an [app password](https://myaccount.google.com/apppasswords). Note that this requires having [2-step verification](https://myaccount.google.com/signinoptions/two-step-verification/enroll-welcome) enabled in your account.

Use the following Commands to store the credentials in user secrets 👇

```powershell
dotnet user-secrets --project src/TopicTalks.Api set "EmailSettings:Email" "you@gmail.com"
dotnet user-secrets --project src/TopicTalks.Api set "EmailSettings:Password" "app-password"
```

> If your SQL server is running on a different port, set the [server and port](src/TopicTalks.Api/appsettings.json#L11-L17) as well accordingly.

#### 4. Setup Google Cloud API Credentials (Optional) ☁️

✅ Free forever.<br>

After researching free cloud storage options, Google Drive storage through Google Cloud seemed to be the best fit for this project. With the limited resources available on the internet for interacting with the Google Drive APIs, it took me a while to understand and [implement the functionalities](src/TopicTalks.Infrastructure/Services/Cloud/GoogleCloud.cs).

However, in order for the project to run with full functionality, you'll need to have your own Google Drive API credentials inserted in the [GoogleCredentials.json](src/TopicTalks.Api/GoogleCredentials.json). <b>Here are the steps to follow to create a free Google Cloud project and get the credentials</b>

1. Create a project in the [Google Cloud Console](https://console.cloud.google.com/projectcreate)
2. Enable [Google Drive API](https://console.cloud.google.com/apis/library/drive.googleapis.com) for the project<br>
   <img src="assets/google/1.png" style="width: 35%"><br>
3. Select `Create Credentials`<br>
   <img src="assets/google/2.png" style="width: 35%"><br>
4. Select `Application Data` -> Click `Next`<br>
   <img src="assets/google/3.png" style="width: 35%"><br>
5. Fill out the details -> Select `Owner` in Role -> Click `Done`<br>
   <img src="assets/google/4.png" style="width: 35%"><br>
6. Go to [Service Accounts](https://console.cloud.google.com/iam-admin/serviceaccounts) -> Select the newly created service account
7. Select `Keys` -> Click `Add Key` -> Choose JSON -> Click `Create`<br>(The credentials `json` file should be automatically downloaded)<br>
   <img src="assets/google/5.png" style="width: 35%"><br>
8. Open the file and copy the value of `client_email`
9. Go to [Google Drive](https://drive.google.com/drive) -> Create a folder named `TopicTalks`
10. Share the folder with the `client_email`<br>
    <img src="assets/google/6.png" style="width: 35%"><br>
    and **General Access** to `Anyone with the link` <br>
11. Finally, paste everything from the downloaded `json` file into [GoogleCredentials.json](src/TopicTalks.Api/GoogleCredentials.json)
12. [Star this project](https://github.com/Raofin/TopicTalks)

#### 5. Build the Projects

```powershell
cd TopicTalks/src
dotnet build TopicTalks.Api/TopicTalks.Api.csproj
dotnet build TopicTalks.Web/TopicTalks.Web.csproj

```

#### 6. Run the Projects (Seperately)

```powershell
dotnet run --project TopicTalks.Api/TopicTalks.Api.csproj --urls "https://localhost:9998"
```

```powershell
dotnet run --project TopicTalks.Web/TopicTalks.Web.csproj --urls "https://localhost:9999"
```

<p align= "center">
  <img src="assets/18.jpg" width="49%">
  <img src="assets/19.jpg" width="49%">
</p>

#### 7. Access the Projects 🌐

- API: https://localhost:9998
- Web: https://localhost:9999

## 🛠️ Technologies and Design Patterns

### Frameworks 🔧

- ASP.NET Core 8.0 Web API
- ASP.NET Core 8.0 MVC
- Entity Framework Core 8.0

### Database 🛢

- Microsoft SQL Server

### Frontend Library 📑

- jQuery
- jQuery Validate
- Bootstrap 5
- Popper.js
- Tippy.js
- FontFace Observer

### Architectural and Design Patterns 📐

- Clean Architecture 🦾
- Result Pattern
- Database Code First Approach with Fluent API
- Repository Pattern
- Unit of Work (UoW)

## 📦 Nuget Packages

| Package Name                                                                   | Used to 👇                                                           |
| ------------------------------------------------------------------------------ | -------------------------------------------------------------------- |
| [Swashbuckle](https://www.nuget.org/packages/Swashbuckle.AspNetCore/)          | Generate API documentation from Web API controllers                  |
| [ErrorOr](https://www.nuget.org/packages/ErrorOr)                              | Handle errors and return results efficiently                         |
| [Serilog](https://www.nuget.org/packages/Serilog)                              | Log events in a very structured way                                  |
| [FluentValidation](https://www.nuget.org/packages/FluentValidation.AspNetCore) | Apply server-side data validation rules                              |
| [FluentEmail](https://www.nuget.org/packages/FluentEmail.Smtp)                 | Send emails using SMTP servers                                       |
| [Google Apis](https://www.nuget.org/packages/Google.Apis.Drive.v3)             | Interact with cloud storage                                          |
| [RazorTemplating](https://www.nuget.org/packages/Razor.Templating.Core)        | Render `.cshtml` files into `string`                                 |
| [DinkToPdf](https://www.nuget.org/packages/DinkToPdf)                          | Generate beautiful PDFs from HTML                                    |
| [ClosedXML](https://www.nuget.org/packages/ClosedXML)                          | Generate Excel (`.xlsx`) files                                       |
| [WebOptimizer](https://www.nuget.org/packages/LigerShark.WebOptimizer.Core)    | Bundle and minify CSS & JavaScript files for faster loading          |
| [WebMarkupMin](https://www.nuget.org/packages/WebMarkupMin.AspNetCore8)        | Minify MVC HTML content to reduce file size for improved performance |

## 📊 ER Diagram

<img src="assets/diagram.svg" title="TopicTalks Logo" alt="TopicTalks Logo" width="1200">

## 📸 Screenshots

View at 👉 [be.net/TopicTalks](https://www.be.net/gallery/195808869/TopicTalks)

<p align= "center">
    <img src="assets/01.jpg">
    <img src="assets/02.jpg">
    <img src="assets/03.jpg">
    <img src="assets/04.jpg">
    <img src="assets/05.jpg">
    <img src="assets/06.jpg">
    <img src="assets/07.jpg">
    <img src="assets/08.jpg">
    <img src="assets/09.jpg">
    <img src="assets/10.jpg">
    <img src="assets/11.jpg">
    <img src="assets/12.jpg">
    <img src="assets/13.jpg">
    <img src="assets/14.jpg">
    <img src="assets/15.jpg">
    <img src="assets/16.jpg">
    <img src="assets/17.jpg">
<p/>

## 🪪 License

Distributed under the BSD 3-Clause License. See [LICENSE](LICENSE) for more information.
