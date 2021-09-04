cd ./Avaliacao.Web/

Write-Host "Restaurando pacotes do npm"
npm i

Write-Host "Criando bundles dos arquivos javascript e css"
gulp build

cd ..

Write-Host "Publicando aplicacao"
dotnet publish ./Avaliacao.Web/ -c Release -o ./avaliacao-built

Write-Host "Copiando arquivo de inicializacao do banco de dados"
Copy-Item ./banco-dados.sql avaliacao-built/banco-dados.sql

Write-Host "Sucesso!"
Write-Host ""
Write-Host ""
Write-Host "Passos seguintes:" -Foreground Blue
Write-Host "	: Execute o arquivo banco-dados.sql na pasta 'avaliacao-built/banco-dados.sql'" -Foreground Blue
Write-Host "	: Atualize a connection string no arquivo de contiguracao 'avaliacao-built/appsettings.json'" -Foreground Blue
Write-Host "	: Execute a aplicacao em: 'avaliacao-built/Avaliacao.Web.exe'" -Foreground Blue

pause