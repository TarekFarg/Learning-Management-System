# 📚 Learning Management System (LMS) API


A **Learning Management System (LMS)** built with **ASP.NET Core Web API**.  
This project provides APIs for managing users, courses, assignments, enrollments, and authentication with role-based authorization.

---

## 🚀 Features
- User authentication & authorization with JWT
- Role-based access control (Admin, Instructor, Student)
- Course management (create, update, delete, list)
- Assignment creation & submissions
- Student enrollment in courses
- Role management for users
- RESTful API with Swagger documentation

---

## 🛠️ Tech Stack
- **ASP.NET Core 9.0**  
- **Entity Framework Core** (Code First + Migrations)  
- **Identity** for user & role management  
- **JWT Authentication**  
- **Swagger / OpenAPI 3.0**  
- **SQL Server** (default database)  

---

## 📂 Project Structure

```

LearningManagementSystem/
│
├── Controllers/              # API Controllers
│   ├── AssignmentSubmissionsController.cs
│   ├── AssignmentsController.cs
│   ├── AuthController.cs
│   ├── CoursesController.cs
│   ├── EnrollmentsController.cs
│   └── UsersController.cs
│
├── Data/
│   └── ApplicationDbContext.cs
│
├── Dtos/                     # Data Transfer Objects
│   ├── AddRoleDto.cs
│   ├── AuthDto.cs
│   ├── CourseDto.cs
│   ├── AssignmentDto.cs
│   ├── EnrollmentDto.cs
│   └── ...
│
├── Helpers/
│   └── JWT.cs
│
├── Models/                   # Database models
│   ├── ApplicationUser.cs
│   ├── Course.cs
│   ├── Assignment.cs
│   ├── AssignmentSubmission.cs
│   └── Enrollment.cs
│
├── Services/                 # Business logic
│   ├── AuthService.cs
│   ├── CourseService.cs
│   ├── AssignmentService.cs
│   ├── EnrollmentService.cs
│   ├── UserService.cs
│   └── Interfaces...
│
├── Migrations/               # EF Core migrations
├── Program.cs
├── appsettings.json
└── README.md

```

---

## 📖 API Documentation

### 🔑 Auth
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST   | `/api/Auth/Register` | Register a new user |
| POST   | `/api/Auth/Login`    | Login & get JWT token |

### 👥 Users
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET    | `/api/Users/user/{id}` | Get user by ID |
| PUT    | `/api/Users/update/{id}` | Update user |
| DELETE | `/api/Users/delete/{id}` | Delete user |
| GET    | `/api/Users/roles/{id}` | Get user roles |
| POST   | `/api/Users/addRole` | Add role to user |
| DELETE | `/api/Users/roles/delete/{userId}` | Remove user role |

### 📚 Courses
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET    | `/api/Courses/AllCourses` | Get all courses |
| GET    | `/api/Courses/CourseID/{id}` | Get course by ID |
| GET    | `/api/Courses/InstructorId/{id}` | Get courses by instructor |
| POST   | `/api/Courses/CreateCourse` | Create new course |
| PUT    | `/api/Courses/UpdateCourse/{id}` | Update course |
| DELETE | `/api/Courses/DeleteCourse/{id}` | Delete course |

### 📝 Assignments
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET    | `/api/Assignments/assignment/{id}` | Get assignment by ID |
| GET    | `/api/Assignments/courseAssignments/{courseId}` | Get course assignments |
| POST   | `/api/Assignments/Create` | Create assignment |
| PUT    | `/api/Assignments/update/{assignmentId}` | Update assignment |
| DELETE | `/api/Assignments/delete/{id}` | Delete assignment |

### 📤 Assignment Submissions
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET    | `/api/AssignmentSubmissions/submission/{id}` | Get submission by ID |
| GET    | `/api/AssignmentSubmissions/assignment/{assId}` | Get submissions for an assignment |
| POST   | `/api/AssignmentSubmissions/create` | Submit assignment |
| PUT    | `/api/AssignmentSubmissions/correct` | Correct submission |
| DELETE | `/api/AssignmentSubmissions/{id}` | Delete submission |

### 🎓 Enrollments
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET    | `/api/Enrollments/courseEnrollments/{courseId}` | Get enrollments by course |
| GET    | `/api/Enrollments/studentEnrollments/{studentId}` | Get student enrollments |
| GET    | `/api/Enrollments/enrollment/{id}` | Get enrollment by ID |
| POST   | `/api/Enrollments/Enroll` | Enroll student in course |
| DELETE | `/api/Enrollments/{id}` | Delete enrollment |

---

## ⚡ Setup & Run

1. **Clone the repo**
   ```bash
   git clone https://github.com/TarekFarg/Learning-Management-System.git
   cd Learning-Management-System
   ```


2. **Update Database**

   * Set your connection string in `appsettings.json`
   * Run EF Core migrations:

     ```bash
     dotnet ef database update
     ```

3. **Run the project**

   ```bash
   dotnet run
   ```

4. **Swagger UI**

   * Navigate to: `https://localhost:44325/swagger/index.html`

---

## ✅ Example Usage

### Register User

```http
POST /api/Auth/Register
{
  "firstName": "Fname",
  "lastName": "Lname",
  "username": "student1",
  "email": "student1@example.com",
  "password": "Pass@123"
}
```

### Login

```http
POST /api/Auth/Login
{
  "email": "student1@example.com",
  "password": "Pass@123"
}
```

*Response:*

```json
{
  "message": null,
  "isAuthenticated": true,
  "username": "student1",
  "email": "student1@example.com",
  "roles": [
    "User",
    "Student"
  ],
  "token": "....."
  "expiresOn": "2025-10-02T11:30:00Z"
}
```

---


## 👤 Author

**Tarek Mohamed**
[GitHub](https://github.com/TarekFarg) | [LinkedIn](https://www.linkedin.com/in/tarek-mohamed-325373267/)


