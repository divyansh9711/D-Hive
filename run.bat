@ECHO OFF
dotnet build
dotnet test ./dhive.test/dhive.test.csproj
dotnet run --project ./driver/mc.csproj