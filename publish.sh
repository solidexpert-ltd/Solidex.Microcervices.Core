#!/bin/sh

echo "Configure credentials"
dotnet nuget add source $FEED_URL -n space -u "%JB_SPACE_CLIENT_ID%" -p "%JB_SPACE_CLIENT_SECRET%" --store-password-in-clear-text
VERSION=6.0.$JB_SPACE_EXECUTION_NUMBER

echo "Publish nuget package"
dotnet pack -o ./
dotnet nuget push Solidex.Microservices.Core.6.0.1.nupkg -s space
dotnet nuget push Solidex.Microservices.Core.6.0.1.nupkg -k 4d8b6735-05f8-3eeb-b7ed-023707ca17d9 -s https://repo.solidexpert.ltd/repository/nuget-hosted