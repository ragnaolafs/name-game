#!/bin/bash

# Check if game ID is provided
if [ -z "$1" ]; then
    echo "Usage: $0 <game_id> [sleep_duration]"
    exit 1
fi

GAME_ID="$1"
SLEEP_DURATION="${2:-1}"

while true; do
    ./make_guess.sh $GAME_ID
    sleep $SLEEP_DURATION
done
