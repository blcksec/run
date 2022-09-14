#!/bin/bash

echo 'Creating symlink for embedded sdk...'

# pwd is the source root folder
rm -rf _dotnetsdk 
mkdir _dotnetsdk
ln -s /usr/share/dotnet/sdk/6.0.300 _dotnetsdk/6.0.300

# Satisfy dev.sh SDK conditions
echo .6.0.300 > _dotnetsdk/.6.0.300
ln -s -f /usr/bin/dotnet /usr/share/dotnet/sdk/6.0.300/dotnet

rm -rf _layout
cd src
./dev.sh l

# dev.sh doesn't restore 'Test'
cd Test
dotnet restore 
