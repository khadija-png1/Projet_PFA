#!/bin/bash

PROJECT_DIR=$1
TARGET_URL=$2

RESULT_DIR="results"

mkdir -p "$RESULT_DIR"

echo "================================="
echo "   DEVSECOPS ORCHESTRATOR"
echo "================================="

# -------------------------
# SAST - SEMGREP
# -------------------------
echo "[+] Running SEMGREP..."

bash SAST/semgrep.sh "$PROJECT_DIR"

# -------------------------
# SAST - TRIVY
# -------------------------
echo "[+] Running TRIVY..."

bash SAST/trivy.sh "$PROJECT_DIR"

# -------------------------
# DAST - ZAP
# -------------------------
#echo "[+] Running ZAP..."

#bash DAST/zap.sh "$TARGET_URL"

echo "================================="
echo "   ALL SCANS COMPLETED"
echo "================================="
echo "Results saved in: results/"
echo "================================="