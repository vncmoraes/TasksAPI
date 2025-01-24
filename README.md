# Tasks API
API de tarefas desenvolvida em ASP.NET Core com integração ao SQL Server.

## Requisitos
.NET 8.0

## Criação do banco de dados
O script necessário para a criação da tabela de Tarefas e as procedures estão no arquivo [Task.sql](https://github.com/vncmoraes/TasksAPI/blob/main/Task.sql)

## Instalação e Uso
### 1) Clonar repositório
`git clone https://github.com/vncmoraes/TasksAPI.git`
### ou
Download do projeto zipado [aqui](https://github.com/vncmoraes/TasksAPI/archive/refs/heads/main.zip)

### 2) Confiar certificado local
`dotnet dev-certs https --trust`

### 3) Executar API
`dotnet run`

> O projeto está configurado para utilizar o Swagger, se ele não abrir por padrão poderá ser acessado em: https://localhost:7246/swagger/index.html
