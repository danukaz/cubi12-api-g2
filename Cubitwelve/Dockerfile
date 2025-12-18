# Stage 1: Build environment with SDK
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy only project files first for better layer caching
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Runtime environment with only runtime dependencies
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App

# Install curl for healthcheck
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Create a non-root user for security
RUN addgroup --system --gid 1001 appgroup && \
    adduser --system --uid 1001 --ingroup appgroup appuser

# Copy the published output from the build environment
COPY --from=build-env /App/out .

# Change ownership to non-root user
RUN chown -R appuser:appgroup /App

# Switch to non-root user
USER appuser

# Render inyecta automáticamente la variable PORT
ENV ASPNETCORE_URLS=http://+:${PORT}
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose default port for documentation
EXPOSE 8080

# Healthcheck usando la variable PORT de Render
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:${PORT}/health || exit 1

# Specify the entry point for the application
ENTRYPOINT ["dotnet", "Cubitwelve.dll"]