#!/bin/bash
set -e 

echo "Heartbeat script starting..."

# Configure Git user for committing changes
git config user.name "github-actions[bot]"
git config user.email "github-actions[bot]@users.noreply.github.com"

# Update the timestamp in the heartbeat file
echo "Last heartbeat: $(date -u '+%Y-%m-%d %H:%M:%S UTC')" > heartbeat.txt

# Stage commit and push
git add heartbeat.txt
git commit -m "heartbat: $(date -u '+%Y-%m-%d')"
git push

echo 'Heartbeat done!'
