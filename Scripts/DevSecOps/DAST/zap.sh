#!/bin/bash

TARGET=$1
RESULT_DIR="../results"
mkdir -p "$RESULT_DIR"

echo "========================="
echo "      ZAP SCAN"
echo "========================="

if [ -z "$TARGET" ]; then
  echo "Usage: ./zap.sh <target>"
  exit 1
fi

ZAP_PATH="/d/zap/Zed_Attack_Proxy/zap.bat"

if [ ! -f "$ZAP_PATH" ]; then
  echo "❌ ZAP introuvable"
  exit 1
fi

"$ZAP_PATH" -cmd \
  -quickurl "$TARGET" \
  -quickout "$RESULT_DIR/zap-report.html"

echo "[+] ZAP scan saved in results/"