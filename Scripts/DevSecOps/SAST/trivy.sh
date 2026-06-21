#!/bin/bash

PROJECT_DIR=$1
RESULT_DIR="D:/Projet_PFA/Scripts/DevSecOps/results"

mkdir -p "$RESULT_DIR"

TRIVY="D:/trivy_0.71.1_windows-64bit/trivy.exe"

"$TRIVY" fs "$PROJECT_DIR" \
  --format json \
  -o "$RESULT_DIR/trivy.json"