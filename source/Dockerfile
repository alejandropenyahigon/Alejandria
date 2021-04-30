#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 8085
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Templates/WebAPI/Devon4Net.Application.WebAPI/Devon4Net.Application.WebAPI.csproj", "Templates/WebAPI/Devon4Net.Application.WebAPI/"]
COPY ["Templates/WebAPI/Devon4Net.WebAPI.Implementation/Devon4Net.WebAPI.Implementation.csproj", "Templates/WebAPI/Devon4Net.WebAPI.Implementation/"]
COPY ["Modules/Devon4Net.Infrastructure.Middleware/Devon4Net.Infrastructure.Middleware.csproj", "Modules/Devon4Net.Infrastructure.Middleware/"]
COPY ["Modules/Devon4Net.Infrastructure.Common/Devon4Net.Infrastructure.Common.csproj", "Modules/Devon4Net.Infrastructure.Common/"]
COPY ["Modules/Devon4Net.Infrastructure.Extensions/Devon4Net.Infrastructure.Extensions.csproj", "Modules/Devon4Net.Infrastructure.Extensions/"]
COPY ["Modules/Devon4Net.Infrastructure.Log/Devon4Net.Infrastructure.Log.csproj", "Modules/Devon4Net.Infrastructure.Log/"]
COPY ["Modules/Devon4Net.Infrastructure.JWT/Devon4Net.Infrastructure.JWT.csproj", "Modules/Devon4Net.Infrastructure.JWT/"]
COPY ["Modules/Devon4Net.Infrastructure.UnitOfWork/Devon4Net.Infrastructure.UnitOfWork.csproj", "Modules/Devon4Net.Infrastructure.UnitOfWork/"]
COPY ["Modules/Devon4Net.Infrastructure.SmaxHcm/Devon4Net.Infrastructure.SmaxHcm.csproj", "Modules/Devon4Net.Infrastructure.SmaxHcm/"]
COPY ["Modules/Devon4Net.Infrastructure.CircuitBreaker/Devon4Net.Infrastructure.CircuitBreaker.csproj", "Modules/Devon4Net.Infrastructure.CircuitBreaker/"]
COPY ["Modules/Devon4Net.Infrastructure.CyberArk/Devon4Net.Infrastructure.CyberArk.csproj", "Modules/Devon4Net.Infrastructure.CyberArk/"]
COPY ["Modules/Devon4Net.Infrastructure.FluentValidation/Devon4Net.Infrastructure.FluentValidation.csproj", "Modules/Devon4Net.Infrastructure.FluentValidation/"]
COPY ["Modules/Devon4Net.Infrastructure.LiteDb/Devon4Net.Infrastructure.LiteDb.csproj", "Modules/Devon4Net.Infrastructure.LiteDb/"]
COPY ["Modules/Devon4Net.Infrastructure.RabbitMQ/Devon4Net.Infrastructure.RabbitMQ.csproj", "Modules/Devon4Net.Infrastructure.RabbitMQ/"]
COPY ["Modules/Devon4Net.Infrastructure.AnsibleTower/Devon4Net.Infrastructure.AnsibleTower.csproj", "Modules/Devon4Net.Infrastructure.AnsibleTower/"]
COPY ["Modules/Devon4Net.Infrastructure.MediatR/Devon4Net.Infrastructure.MediatR.csproj", "Modules/Devon4Net.Infrastructure.MediatR/"]
COPY ["Modules/Devon4Net.Infrastructure.WebAPI/Devon4Net.Infrastructure.WebAPI.csproj", "Modules/Devon4Net.Infrastructure.WebAPI/"]
COPY ["Modules/Devon4Net.Infrastructure.Grpc/Devon4Net.Infrastructure.Grpc.csproj", "Modules/Devon4Net.Infrastructure.Grpc/"]
COPY ["Modules/Devon4Net.Infrastructure.Cors/Devon4Net.Infrastructure.Cors.csproj", "Modules/Devon4Net.Infrastructure.Cors/"]
COPY ["Modules/Devon4Net.Infrastructure.Kafka/Devon4Net.Infrastructure.Kafka.csproj", "Modules/Devon4Net.Infrastructure.Kafka/"]
COPY ["Modules/Devon4Net.Infrastructure.Swagger/Devon4Net.Infrastructure.Swagger.csproj", "Modules/Devon4Net.Infrastructure.Swagger/"]
RUN dotnet restore "Templates/WebAPI/Devon4Net.Application.WebAPI/Devon4Net.Application.WebAPI.csproj"
COPY . .
WORKDIR "/src/Templates/WebAPI/Devon4Net.Application.WebAPI"
RUN dotnet build "Devon4Net.Application.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Devon4Net.Application.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Devon4Net.Application.WebAPI.dll"]