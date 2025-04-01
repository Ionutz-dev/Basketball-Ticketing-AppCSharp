# 🏀 Basketball Ticket Sales System (C# + WinForms)

This project is a **Windows Forms desktop application** for managing basketball ticket sales. It supports GUI interaction, ticket purchase, seat tracking, and SQLite-based persistent storage.

## 📁 Project Structure
```
CSharpApp/
│
├── CSharpApp.csproj                      # .NET project file
├── App.config                            # Configuration file for database connection (SQLite)
├── identifier.sqlite                     # SQLite database file
├── README.md                             # Project documentation
│
├── DB/
│   └── DBUtils.cs                        # Utility for managing SQLite DB connections
│
├── Model/
│   ├── Match.cs                          
│   ├── Ticket.cs                         
│   └── User.cs                          
│
├── Repository/
│   ├── IMatchRepository.cs
│   ├── ITicketRepository.cs
│   ├── IUserRepository.cs                
│   ├── MatchRepository.cs                
│   └── TicketRepository.cs               
│
├── Service/
│   └── TicketService.cs                  # Business logic layer for ticket operations
│
├── MainForm.cs                           # WinForms GUI logic
├── MainForm.Designer.cs                  # Auto-generated WinForms designer
└── Program.cs                            # Entry point of the application
```

## ✨ Features
- Windows Forms **Graphical User Interface** for real-time interaction.
- **Ticket Purchase Flow**: Select match → Enter customer & seats → Confirm sale.
- **Available Seats Tracking** and automatic match updates.
- **SQLite-based Persistent Storage**.
- **Console Logging** for debugging and tracing repository logic.
- **Configurable Connection String** via `App.config`.

## 🚀 How to Run the App
1. **Clone the Repository**
   ```bash
   git clone <repo-url>
   cd CSharpApp
   ```

2. **Open in Rider or Visual Studio**
   - Ensure `.NET 9.0` SDK is installed.
   - Open the `.csproj` file.

3. **Install Dependencies**
   - Install `System.Data.SQLite` (v1.0.119) via NuGet.

4. **Ensure Database File Exists**
   - Confirm that `identifier.sqlite` exists at the **root** of the project.
   - In `.csproj`, it should be set to:
     ```xml
     <None Update="identifier.sqlite">
         <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
     </None>
     ```

5. **Run the App**
   - Build and Run the solution.
   - The **Basketball Ticket Sales** GUI will open.

## ✅ Homework Requirements Implemented

- ✔️ **Model Classes**: `User`, `Match`, and `Ticket` defined to encapsulate application data.
- ✔️ **Repository Interfaces**: Abstracted data access logic (`IMatchRepository`, `ITicketRepository`, etc).
- ✔️ **Repository Implementations**: SQL logic using `SQLiteConnection`, includes seat updates and match filtering.
- ✔️ **Console Logging**: Output added to repository methods to trace key DB actions.
- ✔️ **Configuration Management**: SQLite connection string managed in `App.config` using `ConfigurationManager`.
- ✔️ **UI Layer**: WinForms-based GUI (`MainForm`) for displaying and interacting with ticket data.
- ✔️ **Service Layer**: Business logic handled by `TicketService`, decoupled from UI and DB layers.

## 🧩 Technologies Used
- .NET 9.0 (Windows Desktop)
- Windows Forms
- SQLite (`System.Data.SQLite`)
- JetBrains Rider / Visual Studio
- C#
