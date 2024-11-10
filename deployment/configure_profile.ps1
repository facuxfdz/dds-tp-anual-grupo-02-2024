param (
    [string]$profileName = $(throw "Por favor, especifica el nombre del perfil SSO que deseas configurar como predeterminado.")
)

# Exportar credenciales en formato PowerShell
try {
    # Ejecutar el comando con el formato `powershell`
    $exportedCredentials = aws configure export-credentials --profile $profileName --format powershell | Out-String
    
    # Verificar si se obtuvo alguna salida
    if (-not $exportedCredentials) {
        throw "Error: No se pudieron exportar las credenciales. Verifica que la sesión esté activa usando 'aws sso login --profile $profileName'."
    }
} catch {
    Write-Output $_
    exit
}

# Evaluar las credenciales como comandos de PowerShell
Invoke-Expression $exportedCredentials

# Configurar el perfil "default" usando aws configure
aws configure set aws_access_key_id $Env:AWS_ACCESS_KEY_ID --profile default
aws configure set aws_secret_access_key $Env:AWS_SECRET_ACCESS_KEY --profile default
aws configure set aws_session_token $Env:AWS_SESSION_TOKEN --profile default

Write-Output "El perfil SSO '$profileName' se ha configurado como el perfil predeterminado en AWS CLI con las credenciales exportadas."
