#!/bin/bash

PROJECT_DIR=$1
RESULT_DIR="results"

mkdir -p "$RESULT_DIR"

echo "=== TRIVY SCAN ==="

TRIVY_PATH="C:/trivy/trivy.exe"

if [ ! -f "$TRIVY_PATH" ]; then
  echo "[ERROR] Trivy not found at $TRIVY_PATH"
  exit 1
fi

"$TRIVY_PATH" fs "$PROJECT_DIR" \
  --format json \
  -o "$RESULT_DIR/trivy.json"

echo "DONE TRIVY"