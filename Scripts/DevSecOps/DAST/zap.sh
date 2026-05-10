#!/bin/bash

echo "================================="
echo "        ZAP DAST SCAN"
echo "================================="

TARGET_URL="http://testasp.vulnweb.com"
RESULT_DIR="/mnt/d/Projet_PFA/Scripts/DevSecOps/DAST/zap-results"

mkdir -p "$RESULT_DIR"

ZAP="/snap/zaproxy/current/zap.sh"

echo "[*] Target: $TARGET_URL"
echo "[*] Output: $RESULT_DIR"

$ZAP -cmd \
  -quickurl "$TARGET_URL" \
  -quickprogress \
  -quickout "$RESULT_DIR/report.xml"

echo "================================="
echo "✔ SCAN TERMINÉ"
echo "✔ XML : $RESULT_DIR/report.xml"
echo "================================="