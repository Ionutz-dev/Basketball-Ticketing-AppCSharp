# Basketball Ticket Sales System (C#)
This is a C# implementation of a ticketing system for basketball matches.

## Project Structure
```
CSharpApp/
│
├── CSharpApp.csproj                    
├── App.config                          # Configuration file for database connection
├── identifier.sqlite                   # SQLite database file
├── README.md                           # Project documentation
│
├── DB/
│   └── DBUtils.cs                      # Utility for database connection management
│
├── Model/
│   ├── Match.cs
│   ├── Ticket.cs
│   └── User.cs
│
├── Repository/
│   ├── Interfaces/
│   │   ├── IMatchRepository.cs
│   │   ├── ITicketRepository.cs
│   │   └── IUserRepository.cs
│   └── MatchRepository.cs              
│
└── Program.cs                          # Entry point of the application
```

## Features
- User authentication (Login/Logout)
- Ticket sales tracking
- Available seat search and management
- Logging via console outputs
- Database connection via configuration file (SQLite)

## How to Run
1. Clone the repository.
2. Open the project in **Rider** or **Visual Studio**.
3. Install dependencies:
    - Via NuGet: `System.Data.SQLite` version `1.0.119`
4. Build the project.
5. Ensure `identifier.sqlite` is located in the `bin/Debug/netX.X/` folder.
6. Run the application.

## Homework Requirements Implemented
- Designed and implemented the **model classes**: `User`, `Match`, and `Ticket`, each representing core entities of the ticket sales system with appropriate properties and constructors.
- Defined **repository interfaces** for data operations related to users, matches, and tickets, ensuring separation of concerns and enabling interaction with a **relational database (SQLite)**.
- Developed the **repository implementation** `MatchRepository`, which performs SQL-based operations such as fetching all matches, updating available seats, and retrieving available matches in descending order.
- Integrated **logging via console outputs** within repository methods to trace key actions such as database queries, updates, and error messages.
- Configured **database connection management** through the `DBUtils` utility class, which retrieves connection parameters from `App.config` using `ConfigurationManager`, allowing flexible, centralized configuration.

