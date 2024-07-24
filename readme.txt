################################################################################################################################################################

Deployment Documentation for Books Management WPF Application
Overview
This documentation provides instructions for deploying the Books Management WPF application. The application manages books, authors, and loans using a WPF front-end, MVVM architecture, and Entity Framework for data access. It uses MySQL as the database management system.

################################################################################

Prerequisites
.NET 8.0 Runtime: Ensure that the .NET 8.0 runtime is installed on the target machine. You can download it from the .NET website.

MySQL Server: Install MySQL Server. You can download it from the MySQL website.

Entity Framework Core Tools: Make sure Entity Framework Core tools are installed. This is required for database migrations.

NuGet Packages: Ensure that all required NuGet packages are restored. These include Microsoft.EntityFrameworkCore and other dependencies.

################################################################################

Database Setup

Connection String : Add the connection string to the OnConfiguring function Inside the class BooksDbContext 

using the Package Manager Console use 
	Add-Migration "Name" 
and 
	update-database to create the data 

################################################################################

Building the Application
Build Solution

Open the solution in Visual Studio or use the .NET CLI.
make the WPF project as the Startup solution
Build the project:
	Dotnet build 
	run the solution