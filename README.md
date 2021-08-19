# Payment-Gateway


## Installation

### Requirements

- .Net 5.0 - [Download](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Docker](https://docs.docker.com/get-docker/) and [Docker Compose](https://docs.docker.com/compose/install/)

### Run Loally

- Clone this repo
- Navigate to the project folder
- Run `dotnet restore`
- Navigate to `src/PaymentGateway.WebAPI`
- Run `dotnet run`

### Run inside docker

- Clone this repo
- Navigate to the project folder
- Run `docker-compose up -d --build`
- Navigate to [https://localhost:3000/swagger](https://localhost:300/swagger)