# API Gateway com Ocelot e Microsserviços

Este projeto demonstra a implementação de uma arquitetura de microsserviços usando API Gateway com Ocelot em .NET. O sistema inclui um gateway e dois microsserviços de exemplo.

## 🚀 Estrutura do Projeto

```
/
├── ApiGateway/              # API Gateway usando Ocelot
├── Service1/                # Serviço de Previsão do Tempo
├── Service2/                # Serviço de Produtos
└── docker-compose.yml       # Configuração Docker
```

## 📋 Pré-requisitos

- Docker
- Docker Compose
- .NET 7.0 SDK (para desenvolvimento)

## 🛠️ Instalação e Execução

1. **Clone o repositório**
```bash
git clone [url-do-repositorio]
cd [nome-do-repositorio]
```

2. **Execute o projeto usando Docker Compose**
```bash
docker-compose up --build
```

3. **Verifique se os serviços estão rodando**
```bash
docker-compose ps
```

## 📦 Serviços Disponíveis

### 1. API Gateway
- Porta: 5000
- Responsável por rotear as requisições para os microsserviços apropriados
- Implementa rate limiting e outras políticas de segurança

### 2. Service1 (WeatherForecast)
- Fornece previsões do tempo aleatórias
- Endpoint: `/service1/api/weatherforecast`
- Método: GET

### 3. Service2 (Products API)
- Gerenciamento completo de produtos (CRUD)
- Endpoint base: `/service2/api/products`
- Métodos disponíveis:
  - GET `/service2/api/products` - Lista todos os produtos
  - GET `/service2/api/products/{id}` - Obtém um produto específico
  - POST `/service2/api/products` - Cria um novo produto
  - PUT `/service2/api/products/{id}` - Atualiza um produto
  - DELETE `/service2/api/products/{id}` - Remove um produto

## 🔍 Exemplos de Uso

### Service1 (WeatherForecast)

```bash
# Obter previsão do tempo
curl http://localhost:5000/service1/api/weatherforecast
```

### Service2 (Products)

```bash
# Listar todos os produtos
curl http://localhost:5000/service2/api/products

# Obter produto específico
curl http://localhost:5000/service2/api/products/1

# Criar novo produto
curl -X POST http://localhost:5000/service2/api/products \
-H "Content-Type: application/json" \
-d '{
    "name": "Novo Produto",
    "description": "Descrição do produto",
    "price": 99.99,
    "stock": 10
}'

# Atualizar produto
curl -X PUT http://localhost:5000/service2/api/products/1 \
-H "Content-Type: application/json" \
-d '{
    "id": 1,
    "name": "Produto Atualizado",
    "description": "Nova descrição",
    "price": 149.99,
    "stock": 15
}'

# Deletar produto
curl -X DELETE http://localhost:5000/service2/api/products/1
```

### PowerShell

```powershell
# Listar produtos
Invoke-RestMethod -Uri "http://localhost:5000/service2/api/products" -Method Get

# Criar produto
$body = @{
    name = "Novo Produto"
    description = "Descrição"
    price = 99.99
    stock = 10
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5000/service2/api/products" `
    -Method Post `
    -Body $body `
    -ContentType "application/json"
```

## 🔧 Configuração

### Ocelot Configuration (ocelot.json)
```json
{
  "Routes": [
    {
      "DownstreamPathTemplate": "/api/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "service1",
          "Port": 80
        }
      ],
      "UpstreamPathTemplate": "/service1/{everything}",
      "UpstreamHttpMethod": [ "GET", "POST", "PUT", "DELETE" ]
    },
    {
      "DownstreamPathTemplate": "/api/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "service2",
          "Port": 80
        }
      ],
      "UpstreamPathTemplate": "/service2/{everything}",
      "UpstreamHttpMethod": [ "GET", "POST", "PUT", "DELETE" ]
    }
  ],
  "GlobalConfiguration": {
    "BaseUrl": "http://localhost:5000"
  }
}
```

## 📝 Logs e Monitoramento

Para visualizar logs:
```bash
# Logs do gateway
docker-compose logs api-gateway

# Logs do Service1
docker-compose logs service1

# Logs do Service2
docker-compose logs service2

# Logs em tempo real
docker-compose logs -f
```

## 🛡️ Segurança

- Rate Limiting implementado no Gateway
- CORS configurado para desenvolvimento
- Possibilidade de adicionar autenticação JWT (preparado na estrutura)

## 📚 Documentação Adicional

- [Documentação do Ocelot](https://ocelot.readthedocs.io/)
- [.NET Core Documentation](https://docs.microsoft.com/en-us/dotnet/core/)
- [Docker Documentation](https://docs.docker.com/)

## 🤝 Contribuindo

1. Faça o fork do projeto
2. Crie sua Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a Branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## ⚠️ Notas Importantes

- Este é um projeto de exemplo e pode precisar de ajustes para ambiente de produção
- As configurações de segurança são básicas e devem ser aprimoradas para produção
- O Service2 usa armazenamento em memória e deve ser modificado para usar um banco de dados real em produção

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.
