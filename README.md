# StudentCourseHub

This project is a simple Student and Course Management System implemented using ASP.NET Core Minimal APIs. It provides endpoints to manage students and courses, as well as to associate students with courses.


- CRUD operations for students and courses
- Adding students to courses and removing students from courses

## Technologies Used

- ASP.NET Core 8
- Entity Framework Core
- C#
- JSON

## Getting Started

To run the project locally, follow these steps:

1. Clone the repository to your local machine.
2. Open the project in your preferred IDE (e.g., Visual Studio, Visual Studio Code).
3. Build the project to restore dependencies with `dotnet restore`.
4. Configure the database connection string in the `appsettings.json` file.
5. Apply migrations to create the database schema.
6. Run the application with `dotnet run`.

## API Endpoints

- **Students**
    - `GET /students`: Retrieve all students.
    - `GET /students/{id}`: Retrieve a specific student by ID.
    - `POST /students`: Create a new student.
    - `PUT /students/{id}`: Update an existing student.
    - `DELETE /students/{id}`: Delete a student.
- **Courses**
    - `GET /courses`: Retrieve all courses.
    - `GET /courses/{id}`: Retrieve a specific course by ID.
    - `POST /courses`: Create a new course.
    - `PUT /courses/{id}`: Update an existing course.
    - `DELETE /courses/{id}`: Delete a course.
- **Course-Student Relationships**
    - `POST /courses/{course_id}/students/{student_id}`: Add a student to a course.
    - `DELETE /courses/{course_id}/students/{student_id}`: Remove a student from a course.

## Contribution Guidelines

Contributions to this project are welcome! If you find a bug or have a feature request, please open an issue or submit a pull request.

