# Student Attendance Management System 🎓📊

A backend-focused **Student Attendance Management System** built with **ASP.NET Core Web API**, designed to manage students, classes, academic years, subjects, teachers, and attendance records through structured RESTful APIs.

The project follows a layered architecture to separate API controllers, business logic, data access, entities, and DTOs, making the application easier to maintain, test, and extend.

---

## 🚀 Key Features

### 👨‍🎓 Student Management

* Create, update, retrieve, and delete student records.
* Manage student information using structured DTOs.
* Validate student input data.
* Prevent duplicate student codes.
* Associate students with existing classes.

### 🏫 Class Management

* Create and manage classes.
* Retrieve class information through RESTful APIs.
* Associate students with their respective classes.

### 📅 Academic Year Management

* Create and manage academic year records.
* Retrieve academic year information.
* Support organizing students and academic data by academic year.

### 📚 Subject Management

* Create, update, retrieve, and delete subjects.
* Manage subject information through dedicated APIs.
* Apply request validation using DTOs.

### 👨‍🏫 Teacher Management

* Manage teacher records through RESTful APIs.
* Create, update, retrieve, and delete teacher information.
* Maintain structured teacher-related data.

### 📝 Attendance Management

* Record and manage student attendance.
* Support multiple attendance statuses:

  * Present
  * Absent
  * Late
  * Permission
* Retrieve attendance information through API endpoints.

### 🔐 Authentication & Authorization

* Implemented JWT-based authentication.
* Protected API endpoints using authorization.
* Supports role-based access control for secured application functionality.

### 🧩 Layered Architecture

The application separates responsibilities across different layers:

* API / Controllers
* Services
* Repositories
* Entities
* DTOs
* Infrastructure / Database

This helps maintain separation of concerns and improves application maintainability.

---

## 🛠️ Technology Stack

| Technology                | Purpose                       |
| ------------------------- | ----------------------------- |
| **C#**                    | Application development       |
| **ASP.NET Core Web API**  | RESTful API development       |
| **Entity Framework Core** | ORM and database operations   |
| **SQL Server**            | Relational database           |
| **LINQ**                  | Data querying                 |
| **JWT**                   | Authentication                |
| **Swagger / OpenAPI**     | API documentation and testing |
| **Postman**               | API testing                   |
| **Git & GitHub**          | Version control               |

---

## 🏗️ Architecture

The project follows a layered architecture:

```text
Client
   │
   ▼
ASP.NET Core Web API
   │
   ▼
Controllers
   │
   ▼
Services
   │
   ▼
Repositories
   │
   ▼
Entity Framework Core
   │
   ▼
SQL Server
```

### Responsibilities

**Controllers**

* Handle HTTP requests and responses.
* Expose RESTful API endpoints.

**Services**

* Contain application and business logic.
* Coordinate operations between controllers and repositories.

**Repositories**

* Handle data-access operations.
* Communicate with Entity Framework Core.

**Entities**

* Represent database tables and application domain objects.

**DTOs**

* Define structured request and response models.
* Help control the data exposed through API endpoints.

---

## 📂 Project Structure

```text
StudentAttendance/
│
├── StudentAttendance.Api/
│   ├── Controllers/
│   │   ├── AttendancesController.cs
│   │   ├── AcademicYearsController.cs
│   │   ├── SubjectsController.cs
│   │   └── ...
│   │
│   ├── Program.cs
│   └── appsettings.json
│
├── StudentAttendance.Application/
│   ├── DTOs/
│   ├── Services/
│   └── Interfaces/
│
├── StudentAttendance.Domain/
│   ├── Entities/
│   │   ├── Student.cs
│   │   ├── Class.cs
│   │   ├── AcademicYear.cs
│   │   ├── Attendance.cs
│   │   ├── Subject.cs
│   │   └── Teacher.cs
│   │
│   └── Enums/
│
├── StudentAttendance.Infrastructure/
│   ├── Data/
│   ├── Repositories/
│   └── Migrations/
│
└── README.md
```

> Update the folder names above if your actual solution uses slightly different project names or folders.

---

## 🗄️ Database

The application uses **SQL Server** with **Entity Framework Core** for data persistence.

The database contains entities for managing:

* Students
* Classes
* Academic Years
* Subjects
* Teachers
* Attendance

Entity Framework Core migrations are used to create and update the database schema.

---

## ⚙️ Getting Started

### Prerequisites

Make sure the following are installed:

* .NET SDK
* Visual Studio or Visual Studio Code
* SQL Server
* SQL Server Management Studio (SSMS)
* Git

---

### 1. Clone the Repository

```bash
git clone https://github.com/Dinesh-M43/Student-Attendance-Management-System.git
```

Navigate into the project:

```bash
cd Student-Attendance-Management-System
```

---

### 2. Configure the Database

Open the application's configuration file:

```text
appsettings.json
```

Configure your SQL Server connection string.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=StudentAttendance;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_SERVER` with your SQL Server instance.

**Do not commit passwords or other sensitive credentials to GitHub.**

---

### 3. Restore Dependencies

From the solution directory:

```bash
dotnet restore
```

---

### 4. Apply Database Migrations

Run the Entity Framework Core migration:

```bash
dotnet ef database update
```

If the solution requires specifying the infrastructure and startup projects:

```bash
dotnet ef database update --project StudentAttendance.Infrastructure --startup-project StudentAttendance.Api
```

---

### 5. Run the Application

```bash
dotnet run
```

The API will start on the configured HTTP/HTTPS port.

---

## 📖 API Documentation

The project uses **Swagger/OpenAPI** to document and test the available API endpoints.

After starting the application, open the Swagger endpoint displayed by the application.

Swagger can be used to:

* View available endpoints
* Inspect request/response models
* Test API operations
* Verify authentication-protected endpoints

---

## 🧪 API Testing

API endpoints can be tested using:

* Swagger / OpenAPI
* Postman

Example operations include:

```text
GET     /api/students
GET     /api/students/{id}
POST    /api/students
PUT     /api/students/{id}
DELETE  /api/students/{id}
```

Similar CRUD endpoints are available for the other supported resources.

---

## 🔒 Security

The application includes JWT-based authentication and authorization to protect secured API functionality.

Security-related functionality includes:

* JWT authentication
* Authorization
* Protected API endpoints
* Role-based access control
* Secure API request handling

---

## 🎯 Project Objectives

The project was developed to demonstrate practical experience in:

* Building RESTful APIs with ASP.NET Core
* Developing CRUD-based applications
* Working with Entity Framework Core
* Designing relational database structures
* Implementing DTO-based API communication
* Applying layered architecture
* Implementing JWT authentication
* Working with SQL Server
* Testing APIs using Swagger and Postman
* Applying validation and business rules

---

## 🔮 Future Enhancements

Potential future improvements include:

* Attendance reports and dashboards
* Date-wise and student-wise attendance filtering
* Attendance percentage calculation
* Export attendance reports to Excel/PDF
* Email notifications
* Advanced role and permission management
* Pagination and advanced API filtering
* Frontend application using Angular
* Deployment to a cloud environment

---

## 👨‍💻 Author

**Dinesh M**

Full-Stack .NET Developer

### Technologies

**C# • ASP.NET Core • Web API • Entity Framework Core • SQL Server • Angular • JavaScript**

---

## ⭐ Support

If you find this project useful, consider giving the repository a ⭐ on GitHub.
