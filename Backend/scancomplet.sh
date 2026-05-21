param(
  [string]$ProjectDir
)

$ResultDir = "results"
New-Item -ItemType Directory -Force -Path $ResultDir | Out-Null

Write-Host "=== SEMGREP SCAN ==="

semgrep scan $ProjectDir `
  --config=p/javascript `
  --json | Out-File "$ResultDir\semgrep.json"

Write-Host "=== TRIVY SCAN ==="

trivy fs $ProjectDir `
  --format json `
  -o "$ResultDir\trivy.json"

Write-Host "DONE"