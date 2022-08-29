# VulnView

## SPA Project
Make sure you install all the dependencies (`npm install`). Then, build the project (`npm run build`). Afterwards, copy the content of the directory `Spa/dist` to `Web/Static/Spa`

## Web Project
Fill in the blanks in `appsettings.json.example` then rename it to `appsettings.json`. If the project was not migrated to a relational database, then do so using Entity Framework (`dotnet ef database update -s Web -p Infrastructure`). Finally, run the project `dotnet run --project ./Web`

## Orchestrator Project
Do the same steps outlined in the Web Project section.
