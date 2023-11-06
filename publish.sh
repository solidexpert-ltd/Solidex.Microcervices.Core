#!/bin/sh

echo "Configure credentials"
dotnet nuget add source $FEED_URL -n space -u "%JB_SPACE_CLIENT_ID%" -p "%JB_SPACE_CLIENT_SECRET%" --store-password-in-clear-text
VERSION=6.0.$JB_SPACE_EXECUTION_NUMBER

echo "Publish nuget package"
dotnet pack -o ./
dotnet nuget push Solidex.Microservices.Core.6.0.2.nupkg -s space