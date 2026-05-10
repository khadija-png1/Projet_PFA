#!/bin/bash

echo "================================="
echo "      TRIVY SCAN START"
echo "================================="

# Dossier à scanner (modifiable)
PROJECT_DIR="/mnt/d/Projet_PFA/Scripts/DevSecOps/SAST/trivy-test"

# Dossier de sortie
RESULT_DIR="results"

mkdir -p "$RESULT_DIR"

echo "[*] Scanning: $PROJECT_DIR"

# Vérification si dossier existe
if [ ! -d "$PROJECT_DIR" ]; then
  echo "❌ Dossier introuvable: $PROJECT_DIR"
  exit 1
fi

# =========================
# 1. Scan JSON (pour backend / Symfony / API)
# =========================
trivy fs "$PROJECT_DIR" \
  --format json \
  -o "$RESULT_DIR/trivy.json"

# =========================
# 2. Scan TXT (lecture humaine)
# =========================
trivy fs "$PROJECT_DIR" \
  --format table \
  -o "$RESULT_DIR/trivy.txt"

echo "================================="
echo "✔ SCAN TERMINÉ"
echo "✔ JSON : $RESULT_DIR/trivy.json"
echo "✔ TXT  : $RESULT_DIR/trivy.txt"
echo "================================="