FROM mcr.microsoft.com/dotnet/sdk:9.0@sha256:3fcf6f1e809c0553f9feb222369f58749af314af6f063f389cbd2f913b4ad556
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
# RUN dotnet restore

EXPOSE 8080
# Build and publish a release
# RUN dotnet publish -o out


#  # Build runtime image
# FROM mcr.microsoft.com/dotnet/runtime:9.0
# WORKDIR /App
# COPY --from=build /App/out .
# ENTRYPOINT ["dotnet", "MyConnectionFactory.dll"]
