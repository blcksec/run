#!/bin/bash

echo 'Creating symlink for embedded sdk...'

# pwd is the source root folder
ln -s /usr/share/dotnet/sdk _dotnetsdk 
echo .6.0.300 >> _dotnetsdk/6.0.300/.6.0.300
# ln -s /home/root/.dotnet/dotnet _dotnetsdk/6.0.300/dotnet

exit 0

cd src
./dev.sh