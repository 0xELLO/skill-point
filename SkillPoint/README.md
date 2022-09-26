# SkillPoint

# RUN
- Run the docket-compose.yml to create DB container
- Apply migrations
- Run the Dockerfile to create APP container

## Usefull commands

### DB

~~~sh
dotnet ef migrations add --project App.DAL.EF --startup-project WebApp --context AppDbContext Initial

dotnet ef migrations remove --project App.DAL.EF --startup-project WebApp --context AppDbContext

dotnet ef database update --project App.DAL.EF --startup-project WebApp

dotnet ef database drop --project App.DAL.EF --startup-project WebApp
~~~
### Controllers

~~~sh
cd WebApp
dotnet aspnet-codegenerator controller -name !!NAME + Controller       -actions -m  App.Domain.!!NAME    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

cd WebApp
dotnet aspnet-codegenerator controller -name !!Name + Controller     -m App.Domain.!!NAME     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f

dotnet aspnet-codegenerator razorpage -m GameConfig -dc ApplicationDbContext -udl -outDir Pages/GameConfiguration --referenceScriptLibraries
~~~
