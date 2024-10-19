# Quiz Application

This is a web-based **Quiz Application** built with **ASP.NET Core** and **Entity Framework Core**. The application allows users to create, take, and manage quizzes across different topics. It also stores user scores and provides a history of quiz attempts.

## Features

- **Create Quizzes**: Users can create quizzes with multiple-choice questions and assign a correct answer for each.
- **Take Quizzes**: Users can take quizzes and receive real-time feedback on their score.
- **Manage Questions**: Users can add, edit, and delete questions from quizzes.
- **Score History**: Users can track their past quiz attempts and view their scores along with the correct and incorrect answers.
- **Randomized Questions**: Quiz questions are shuffled each time a quiz is taken to provide a unique experience.
  
## Technologies Used

- **ASP.NET Core**: For building the web application.
- **Razor Pages**: For server-side rendering and managing the UI.
- **Entity Framework Core**: For database interaction (supports both SQL Server and PostgreSQL).
- **PostgreSQL**: As the default database (can be changed if required).
- **Bootstrap**: For styling the UI components.

## Setup Instructions

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (Version 6 or later)
- [PostgreSQL](https://www.postgresql.org/download/) or any other supported database
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Steps to Run the Project

1. **Clone the repository**:
    ```bash
    git clone https://github.com/yourusername/quiz-application.git
    cd quiz-application
    ```

2. **Set up the database**:
   - Modify the connection string in `appsettings.json` to match your PostgreSQL setup:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=QuizDB;Username=yourusername;Password=yourpassword"
     }
     ```
   - Run the following commands to apply migrations and create the database:
     ```bash
     dotnet ef database update
     ```

3. **Run the application**:
    ```bash
    dotnet run
    ```
    The application should now be running at `https://localhost:5001`.

4. **Access the Application**:
   Open your web browser and navigate to `https://localhost:5001` to access the homepage of the application.
