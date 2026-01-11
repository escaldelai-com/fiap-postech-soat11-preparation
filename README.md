# Microsserviço Restaurant Preparation
Esse microsserviço é responsável por gerenciar a preparação do pedido na cozinha e a entrega para o cliente, encerrando o pedido.

## Infraestrutura
- AspNet core 10.0
- MongoDB
- Docker

## Ambiente

### Compilação
`dotnet build Restaurant.Preparation.WebApi/Restaurant.Preparation.WebApi.csproj`

### Testes
`dotnet test Restaurant.Preparation.Domain.Test/Restaurant.Preparation.Domain.Test.csproj`

`dotnet test Restaurant.Preparation.Application.Test/Restaurant.Preparation.Application.Test.csproj`

### Execução
`dotnet Restaurant.Preparation.WebApi.dll`

### Publicação
`dotnet publish Restaurant.Preparation.WebApi/Restaurant.Preparation.WebApi.csproj -c Release -o dist`

### Docker
`docker build -t restaurant-preparation:{{version}} .`

### Kubernetes

#### MongoDB
`mongo-prep-service.yaml` ClusterIP para comunicação interna com o App

`mongo-prep-secrets.yaml` Usuário e senha do MongoDB

`mongo-prep-configmap.yaml` Inicialização das coleções do MongoDB

`mongo-prep.yaml` StatefulSet do MongoDB

#### App
`app-prep-service.yaml` ClusterIP para comunicação interna com o App

`app-prep-ingress.yaml` Ingress para comunicação externa com o App

`app-prep.yaml` Deployment do App

### Arquitetura
```
app-prep (AspNet Core)
│
├── mongo-prep (MongoDB - Pedidos, produtos, tipos de produto) (get/set)
|
├── app-id (AspNet Core - identificação de clientes) (out)
|
└── app-pay (AspNet Core - Processamento de pagamento) (in)
```

## Endpoints

### Main
- `GET /` retorna `204 No Content` para o teste de vida da API.

### Order
- `GET /order/waiting` Retorna a lista de pedidos aguardando preparação na cozinha.
- `POST /order/confirm` Recebe a confirmação do recebimento do pedido na cozinha.
- `POST /order/prepare` Recebe a confirmação do início da preparação do pedido na cozinha.
- `POST /order/delivery` Recebe a confirmação de que o pedido está aguardando a retirada do cliente.
- `POST /order/finalize` Recebe a confirmação de que o cliente recebeu o pedido e encerra o mesmo.