job(".NET Core sdk. Build, test, publish"){
    container(image = "mcr.microsoft.com/dotnet/sdk:6.0"){
        env["FEED_URL"] = "https://nuget.pkg.jetbrains.space/solidexpert/p/scrm/nuget/v3/index.json"
        shellScript {
            content = """
                echo Run build...
                dotnet build
                echo Run tests...
                dotnet test ./MainTests/
                echo Publish NuGet package...
                chmod +x publish.sh
                ./publish.sh
            """
        }
    }
}