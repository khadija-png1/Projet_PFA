#!/bin/bash

PROJECT_DIR=$1
RESULT_DIR="results"

mkdir -p "$RESULT_DIR"

echo "=== SEMGREP SCAN ==="

semgrep scan "$PROJECT_DIR" \
  --config=p/javascript \
  --json -o "$RESULT_DIR/semgrep.json"

echo "DONE SEMGREP"