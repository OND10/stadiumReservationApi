# Stadium Reservation System 🏟️

A full-stack solution for stadium and pitch reservations, featuring a robust .NET API and a dynamic Flutter mobile application.

## 🚀 Project Overview

This project is designed to streamline the process of booking sport pitches and stadiums. It consists of two main components:
- **Api**: A clean-architecture ASP.NET Core Web API handling business logic, database interactions, and domain rules.
- **App**: A modern Flutter mobile application using GetX for state management, providing a smooth user experience.

## 🏗️ Architecture & Stack

### Backend (.NET API)
- **Framework**: .NET 8.0 / ASP.NET Core
- **Patterns**: Clean Architecture (Domain, Application, Infrastructure, API)
- **Features**: RESTful endpoints, XUnit Testing, Dependency Injection.

### Frontend (Flutter App)
- **Framework**: Flutter
- **State Management**: [GetX](https://pub.dev/packages/get)
- **Key Features**:
  - Zoom Drawer navigation
  - Shared Preferences for local storage
  - Image selection (Image Picker)
  - SVG support
  - Carousel Sliders for pitch showcases

## 📂 Project Structure

```text
ReservationOfpitch/
├── Api/              # .NET Backend Solution
│   ├── ReservationofPitch.API/          # Entry point & Controllers
│   ├── Reservationpitch.Application/    # Business Logic
│   ├── Reservationpitch.Domain/         # Entities & Interfaces
│   ├── Reservationpitch.Infustractur/   # Data Access & Implementation
│   └── Reservationpitch.XUnitTest/      # Automated Tests
├── App/              # Flutter Mobile Application
│   └── stadium_reservation/
└── digram/           # Architecture & Design Diagrams
```

## 🛠️ Getting Started

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (Version 8.0 recommended)
- [Flutter SDK](https://docs.flutter.dev/get-started/install)
- [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### 1. Running the API
1. Navigate to the API directory:
   ```bash
   cd Api
   ```
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Run the project:
   ```bash
   dotnet run --project ReservationofPitch.API
   ```
   *The API should now be running (usually on https://localhost:7071 or similar).*

### 2. Running the Flutter App
1. Navigate to the App directory:
   ```bash
   cd App/stadium_reservation
   ```
2. Get packages:
   ```bash
   flutter pub get
   ```
3. Run the application:
   ```bash
   flutter run
   ```

## 📞 Contact & Repository
- **GitHub**: [OND10/stadiumReservationApi](https://github.com/OND10/stadiumReservationApi)

---
*Developed with ❤️ for stadium owners and sports enthusiasts.*
