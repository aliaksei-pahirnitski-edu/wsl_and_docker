#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# test with Volume
RUN mkdir /vol
RUN echo "Abc" "Hoy Version6!!!" $PATH
RUN echo " hallo $RANDOM in image $PATH building6" >> /vol/my_super_file.txt
VOLUME ["/vol"]

# test with permission
RUN mkdir /abc_readonly && \
	mkdir /abc && \
	chmod -R a+rwx /abc

USER app
WORKDIR /tennis
WORKDIR /app

EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["wsl_and_docker.csproj", "."]
RUN dotnet restore "./wsl_and_docker.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./wsl_and_docker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./wsl_and_docker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "wsl_and_docker.dll", "--some_arg"]