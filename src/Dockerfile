# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia tudo para dentro do container
COPY . .

# Navega até o projeto principal (ajuste se necessário)
WORKDIR /app/src

# Restaura dependências
RUN dotnet restore InvictusAPI.csproj

# Publica o projeto
RUN dotnet publish InvictusAPI.csproj -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia os arquivos publicados da etapa de build
COPY --from=build /app/publish .

# Expõe a porta (pode ajustar para a que você usa)
EXPOSE 80

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "InvictusAPI.dll"]
