#!/bin/bash
date

echo Publishing is starting
dotnet publish -r linux-arm # publish for Raspberry
wait
echo "Build done"

rsync -ru ./bin/Debug/netcoreapp2.1/linux-arm/publish/* pi@192.168.0.241:/home/pi/dotnet/PacktNewsletter # copy only changed files to Raspberry 
wait
echo "Copying application done"

rsync -u credentials.json pi@192.168.0.241:/home/pi/dotnet/PacktNewsletter # copy credentials if changed or not exists
wait
echo "Copying credentials done"

rsync -u recipients.json pi@192.168.0.241:/home/pi/dotnet/PacktNewsletter # copy recipients if changed or not exists
wait
echo "Copying recipients done"

ssh pi@192.168.0.241 << EOF
chmod +x ./dotnet/PacktNewsletter/PacktNewsletterApp.PacktNewsletterConsole
EOF

wait
echo "Done"