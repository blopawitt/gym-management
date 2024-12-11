# Gym Management System

This is a Gym Management System project that includes a frontend built with Vue.js and a backend built with ASP.NET Core.

## Prerequisites

- .NET 6.0 SDK or later
- Node.js 14.x or later
- npm 6.x or later

## Running the Project

### Backend

1. Navigate to the backend directory:

   ```sh
   cd backend/GymManagement
   ```

2. Run the backend using the following command:
   ```sh
   dotnet run --launch-profile https
   ```

### Frontend

1. Navigate to the frontend directory:

   ```sh
   cd frontend
   ```

2. Install the dependencies:

   ```sh
   npm install
   ```

3. Run the frontend using the following command:
   ```sh
   npm run serve
   ```

## Additional Information

- The backend uses JWT for authentication.
- The frontend uses Vue.js with various plugins and libraries such as FullCalendar, Axios, and PrimeVue.
- Ensure that the connection strings and JWT settings are properly configured in the backend `appsettings.json` file.
