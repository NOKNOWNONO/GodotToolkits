#!/bin/bash

dotnet run --project ./InfoSet/InfoSet.csproj

dotnet restore src/GodotToolkits.MVVM/GodotToolkits.MVVM.csproj
dotnet restore src/GodotToolkits.I18N/GodotToolkits.I18N.csproj

dotnet build src/GodotToolkits.I18N/GodotToolkits.I18N.csproj --configuration Release
dotnet build src/GodotToolkits.MVVM/GodotToolkits.MVVM.csproj --configuration Release
