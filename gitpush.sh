#!/bin/bash

cd /c/Users/dskripnikov/Desktop/mce/

REPO_URL="https://github.com/realskrip/Motorsport-championship-editor-ASP.NET-MVC.git"

while true; do
    git push $REPO_URL

    if [ $? -eq 0 ]; then
        echo "Push successful!"
        break
    else
        echo "Push failed. Retrying in 2 seconds..."
        sleep 2
    fi
done