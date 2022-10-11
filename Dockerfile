FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
WORKDIR "/src"
ENV ConnectionStrings__SQLServer=""
ENTRYPOINT ["dotnet", "test", "gRPC.Tests/gRPC.Tests.csproj"]
