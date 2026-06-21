#!/bin/bash

PROJECT_DIR=$1
RESULT_DIR="D:/Projet_PFA/Scripts/DevSecOps/results"

mkdir -p "$RESULT_DIR"

# activer venv (IMPORTANT)
source "D:/Projet_PFA/Scripts/DevSecOps/SAST/venv/Scripts/activate"

# utiliser python du venv
semgrep scan "$PROJECT_DIR" \
  --config=p/javascript \
  --json > "$RESULT_DIR/semgrep.json"