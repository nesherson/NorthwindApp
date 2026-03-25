# Northwind application

A simple CRUD app based on the Northwind database.
Backend is built using ASP.NET Core and frontend is built with React and Vite.

## Features

- **Product Management**: View, create, update, and delete products
- **Category Management**: Organize products by categories
- **Supplier Management**: Manage supplier information
- **Order Management**: Create and track customer orders
- **Customer Management**: View and manage customer details
- **Responsive Design**: Works seamlessly on desktop and mobile devices
- **RESTful API**: Clean and well-structured backend API
- **Modern Frontend**: Built with React 18 and Vite for optimal performance

## Installation

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download) or later
- [Node.js](https://nodejs.org/) 16.x or later
- [npm](https://www.npmjs.com/) or [yarn](https://yarnpkg.com/)

### Backend Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/nesherson/NorthwindApp.git
   cd NorthwindApp
   ```

2. Navigate to the backend directory:
   ```bash
   cd NorthwindApp.Server
   ```

3. Install dependencies:
   ```bash
   dotnet restore
   ```

4. Update the database connection string in `appsettings.json` if needed

5. Run migrations:
   ```bash
   dotnet ef database update
   ```

6. Start the backend server:
   ```bash
   dotnet run
   ```
   The API will be available at `https://localhost:5001`

### Frontend Setup

1. Navigate to the frontend directory:
   ```bash
   cd NorthwindApp.Client
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm run dev
   ```
   The application will be available at `http://localhost:5173`

## Usage

Once both servers are running, open your browser and navigate to `http://localhost:5173` to access the Northwind application. You can now manage products, categories, suppliers, orders, and customers through the user-friendly interface.