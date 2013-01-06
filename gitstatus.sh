#!/bin/bash

find Vanaheimr -maxdepth 2 -type d -name .git | xargs -n 1 dirname | while read line; do echo "Checking: $line" && cd $line && git status; cd ..; cd ..; done
