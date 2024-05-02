# InventoryManagementSystem
Setup Backend:
Need Visual Studio and SQL server for the backend.
1. Configure connection string in appsettings.json according to your environment

Execute Db Migrations:
Following commands will create the products, sales and purchases tables:
Run command in terminal i.e. dotnet ef migrations add <Your-Migration-Name>
Then run command i.e. dotnet ef database update
Manually add some data in products and sales table.

Build and Run the project which will open Swagger API and there all the http requests can be tested.

Frontend Setup:
Open frontend project in an IDE like Visual studio code and run the command 'ng serve' in the terminal to start the application.
