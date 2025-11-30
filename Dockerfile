# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ScmDesignli.sln ./
COPY ScmDesignli.Api/ScmDesignli.Api.csproj ScmDesignli.Api/
COPY ScmDesignli.Application/ScmDesignli.Application.csproj ScmDesignli.Application/
COPY ScmDesignli.Domain/ScmDesignli.Domain.csproj ScmDesignli.Domain/
COPY ScmDesignli.Infrastructure/ScmDesignli.Infrastructure.csproj ScmDesignli.Infrastructure/
COPY ScmDesignli.UnitTest/ScmDesignli.UnitTest.csproj ScmDesignli.UnitTest/

# Restore dependencies
RUN dotnet restore

# Copy all source code
COPY . .

# Run unit tests
WORKDIR /src/ScmDesignli.UnitTest
RUN dotnet test --configuration Release --no-restore --verbosity normal

# Build the application
WORKDIR /src/ScmDesignli.Api
RUN dotnet build -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy published files from publish stage
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1

# Run the application
ENTRYPOINT ["dotnet", "ScmDesignli.Api.dll"]
