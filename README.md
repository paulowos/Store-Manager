
# Store Manager

Sistema de gerenciamento de vendas em que será possível criar, visualizar, deletar e atualizar produtos e vendas

## Tech Stack

**API:** .Net, AspNet, C#, Entity Framework, SQL Server

**Tests:** XUnit, Moq, Fluent Assertions


## Rodar Localmente

Clone o projeto

```bash
  git clone https://github.com/paulowos/Store-Manager.git
```

Vá para o diretório do projeto

```bash
  cd Store-Manager
```

Inicie a aplicação

```bash
  docker compose up -d --build
```


## Rodando Testes

Para rodar os testes, execute os seguintes comandos

```bash
  cd StoreManager.Test
```
```bash
  dotnet test
```


## API Referência
- Para documentação completa, rode a aplicação localmente e acesse:
```http
    /swagger/index.html
```

___

#### Get all products

- Retorna todos os produtos cadastrados 

```http
  GET /products
```

#### Get product

- Retorna o produto com o Id especificado

```http
  GET /products/{id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int`    | (**Required**) Product Id |


#### Search product

- Busca um produto pelo nome

```http
  GET /products/search
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `q`      | `string`    | (**Required**) Product Name |


#### Add product

- Cadastra um novo produto

```http
  POST /products
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `product`      | `json`    | (**Required**) Product Info |


#### Update product

- Atualiza um produto

```http
  PUT /products/{id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
|`id`       |`int`     |(**Required**) Product Id|
| `product` | `json`    | (**Required**) Product Info |


#### Remove product

- Remove um produto

```http
  DELETE /products/{id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int`    | (**Required**) Product Id |

#### Get all sales

- Retorna todas as vendas cadastradas 

```http
  GET /sales
```

#### Get sale

- Retorna a venda com o Id especificado

```http
  GET /sales/{id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int`    | (**Required**) Sale Id |


#### Add sale

- Cadastra uma nova venda

```http
  POST /sales
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `sale`      | `json`    | (**Required**) Sale Info |


#### Remove sale

- Remove uma venda

```http
  DELETE /sales/{id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int`    | (**Required**) Sale Id |


## Feedback

Se você possui algum feedback, por favor me contate através do email paulowos@gmail.com, ou [linkedin](https://www.linkedin.com/in/paulowos/)

