#!/bin/bash

echo "================================="
echo "   SEMGREP SCAN START"
echo "================================="

PROJECT_DIR="/mnt/d/Projet_PFA/Backend"
RESULT_DIR="results"

mkdir -p "$RESULT_DIR"

echo "Scanning: $PROJECT_DIR"

# 🔴 JSON propre (recommandé)
semgrep scan \
  --config=p/javascript \
  "$PROJECT_DIR" \
  --json -o "$RESULT_DIR/semgrep.json"

# 🟡 TXT lisible (console log)
semgrep scan \
  --config=p/javascript \
  "$PROJECT_DIR" \
  | tee "$RESULT_DIR/semgrep.txt" > /dev/null

# vérifier JSON
if [ -s "$RESULT_DIR/semgrep.json" ]; then
    echo "✔ Scan OK (résultats générés)"
    echo "✔ JSON : $RESULT_DIR/semgrep.json"
    echo "✔ TXT  : $RESULT_DIR/semgrep.txt"
else
    echo "❌ Aucun résultat ou scan échoué"
fi

echo "================================="