#!/bin/bash

echo 'Creating symlink for embedded sdk...'

# pwd is the source root folder
rm -rf _dotnetsdk 
ln -s /usr/share/dotnet/sdk/6.0.300 _dotnetsdk 

# Satisfy dev.sh SDK conditions
echo .6.0.300 > _dotnetsdk/6.0.300/.6.0.300
ln -s /usr/bin/dotnet /usr/share/dotnet/sdk/6.0.300/dotnet

cd src
./dev.sh

dotnet restore ActionsRunner.sln
