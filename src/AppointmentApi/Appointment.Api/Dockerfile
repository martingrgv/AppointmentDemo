FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY [ "AppointmentApi/Appointment.Api/Appointment.Api.csproj", "AppointmentApi/Appointment.Api/" ]
COPY [ "AppointmentApi/Appointment.Domain/Appointment.Domain.csproj", "AppointmentApi/Appointment.Domain/" ]
COPY [ "AppointmentApi/Appointment.Infrastructure/Appointment.Infrastructure.csproj", "AppointmentApi/Appointment.Infrastructure/" ]
RUN dotnet restore "AppointmentApi/Appointment.Api/Appointment.Api.csproj"
COPY . .
WORKDIR "/src/AppointmentApi/Appointment.Api/"
RUN dotnet build "Appointment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Appointment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Appointment.Api.dll" ]
