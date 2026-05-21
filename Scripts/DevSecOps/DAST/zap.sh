#!/bin/bash

echo "=== ZAP SCAN ==="

TARGET_URL=$1
RESULT_DIR="results"

mkdir -p "$RESULT_DIR"

ZAP_DIR="/c/Program Files/ZAP/Zed Attack Proxy"

if [ ! -f "$ZAP_DIR/zap.bat" ]; then
  echo "❌ ZAP introuvable dans $ZAP_DIR"
  exit 1
fi

# 🔥 se placer dans le dossier ZAP
cd "$ZAP_DIR" || exit 1

./zap.bat -cmd \
  -quickurl "$TARGET_URL" \
  -quickprogress \
  -quickout "/d/Projet_PFA/Scripts/DevSecOps/results/zap-report.html"

echo "ZAP DONE"