
**SCAFFOLD**

dotnet ef dbcontext Scaffold "Server=localhost;Database=VehicleManagement;TrustServerCertificate=True;User ID=sa;Password=Sunshine1;" Microsoft.EntityFrameworkCore.SqlServer --output-dir DbModels -f --context "VehicleManagementContext" --context-dir ./ --use-database-names --no-onconfiguring


**Windows**

dotnet ef dbcontext scaffold "Server=.\DEV;Database=VehicleManagement;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir DbModels -f --context "VehicleManagementContext" --context-dir ./ --use-database-names --no-onconfiguring
