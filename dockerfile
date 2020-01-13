FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://+:5000;

EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build

WORKDIR /src
RUN mkdir /src/Xend.CRM
RUN mkdir /src/Xend.CRM/Xend.CRM.Common
RUN mkdir /src/Xend.CRM/Xend.CRM.Core
RUN mkdir /src/Xend.CRM/Xend.CRM.DataAccessLayer
RUN mkdir /src/Xend.CRM/Xend.CRM.ModelLayer
RUN mkdir /src/Xend.CRM/Xend.CRM.ServiceLayer
RUN mkdir /src/Xend.CRM/Xend.CRM.Test.Integration
RUN mkdir /src/Xend.CRM/Xend.CRM.Test.Unit
RUN mkdir /src/Xend.CRM/Xend.CRM.WebApi



COPY ["Xend.CRM/Xend.CRM.Common/Xend.CRM.Common.csproj", "Xend.CRM/Xend.CRM.Common"]
COPY ["Xend.CRM/Xend.CRM.Core/Xend.CRM.Core.csproj", "Xend.CRM/Xend.CRM.Core"]
COPY ["Xend.CRM/Xend.CRM.DataAccessLayer/Xend.CRM.DataAccessLayer.csproj", "Xend.CRM/Xend.CRM.DataAccessLayer"]
COPY ["Xend.CRM/Xend.CRM.ModelLayer/Xend.CRM.ModelLayer.csproj", "Xend.CRM/Xend.CRM.ModelLayer"]
COPY ["Xend.CRM/Xend.CRM.ServiceLayer/Xend.CRM.ServiceLayer.csproj", "Xend.CRM/Xend.CRM.ServiceLayer"]
COPY ["Xend.CRM/Xend.CRM.Test.Integration/Xend.CRM.Test.Integration.csproj", "Xend.CRM/Xend.CRM.Test.Integration"]
COPY ["Xend.CRM/Xend.CRM.Test.Unit/Xend.CRM.Test.Unit.csproj", "Xend.CRM/Xend.CRM.Test.Unit"]
COPY ["Xend.CRM/Xend.CRM.WebApi/Xend.CRM.WebApi.csproj", "Xend.CRM/Xend.CRM.WebApi"]
COPY ["Xend.CRM.sln", "./"]

RUN dotnet restore Xend.CRM.sln


COPY . ./
RUN dotnet build "Xend.CRM/Xend.CRM.WebApi/Xend.CRM.WebApi.csproj" -c Release -o /app


FROM build AS publish
RUN dotnet publish "Xend.CRM/Xend.CRM.WebApi/Xend.CRM.WebApi.csproj" -c Release -o /app


FROM base AS final

WORKDIR /app
# ADD https://github.com/kelseyhightower/confd/releases/download/v0.10.0/confd-0.10.0-linux-amd64 /usr/local/bin/confd
# RUN chmod +x /usr/local/bin/confd
# ADD docker/etc/confd /etc/confd
# ADD docker/run.sh /run.sh
# RUN chmod +x /run.sh

COPY --from=publish /app .

CMD ["/run.sh"]
