# Script para limpiar CSS embebido de todas las vistas
$viewsPath = "Views"

# Función para limpiar una vista
function Clean-View {
    param($filePath)
    
    Write-Host "Limpiando: $filePath"
    
    $content = Get-Content $filePath -Raw
    
    # Eliminar etiquetas head completas
    $content = $content -replace '<head>.*?</head>', '' -replace '(?s)<head>.*?</head>', ''
    
    # Eliminar etiquetas style completas
    $content = $content -replace '(?s)<style>.*?</style>', ''
    
    # Limpiar líneas en blanco múltiples
    $content = $content -replace "`r`n`r`n`r`n", "`r`n`r`n"
    $content = $content -replace "`n`n`n", "`n`n"
    
    # Guardar el archivo limpio
    Set-Content $filePath $content -Encoding UTF8
    
    Write-Host "✓ Limpiado: $filePath"
}

# Obtener todas las vistas .cshtml
$views = Get-ChildItem -Path $viewsPath -Filter "*.cshtml" -Recurse

foreach ($view in $views) {
    $content = Get-Content $view.FullName -Raw
    
    # Solo procesar si tiene CSS embebido
    if ($content -match '<style>' -or $content -match '<head>') {
        Clean-View $view.FullName
    }
}

Write-Host "Limpieza completada!" 