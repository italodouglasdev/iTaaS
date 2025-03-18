# ItaaS - Conversor de Logs API

O projeto ItaaS segue uma arquitetura bem organizada, separando responsabilidades em diferentes camadas para manter um código limpo, modular e de fácil manutenção. Abaixo está um resumo das principais camadas e seus respectivos papéis:

## 🚀 ItaaS.Api
Este é o projeto principal da API, onde estão configurados os serviços e endpoints para exposição dos recursos.

### 📂 Aplicacao (Camada de Aplicação)
Contém as regras de negócio e a lógica de manipulação de dados antes de chegar à camada de infraestrutura.

- **DTOs**: Objetos de transferência de dados usados para comunicação entre camadas.
- **Interfaces**: Definições de contratos que são implementados pelos serviços.
- **Mapeadores**: Responsáveis por converter objetos entre diferentes camadas.
- **Serviços**: Implementação da lógica de negócio.
- **Validadores**: Classes para validação de dados.

### 📂 Controllers (Camada de Apresentação)
Contém os endpoints da API que lidam com as requisições HTTP e direcionam para os serviços correspondentes.

### 📂 Dominio (Camada de Domínio)
Define o núcleo do sistema com as regras fundamentais.

- **Entidades**: Representações das tabelas do banco de dados.
- **Enumeradores**: Definições de valores fixos utilizados no sistema.
- **Fabricas**: Padrões para criação de objetos complexos.
- **Helpers**: Classes auxiliares com métodos utilitários.

### 📂 Infraestrutura (Camada de Acesso a Dados)
Responsável pela comunicação com o banco de dados e armazenamento de informações.

- **BancoDeDados**: Configurações do banco.
- **Migrations**: Scripts de versionamento do banco de dados (usando Entity Framework).
- **Repositorios**: Implementações de acesso aos dados, seguindo o padrão Repository.

## 📂 ItaaS.Testes
Este projeto contém os testes automatizados da aplicação, garantindo a qualidade e o funcionamento correto do sistema.

- **Mocks**: Objetos simulados para testar funcionalidades sem acessar diretamente a base de dados.
- **Testes**: Implementação dos casos de teste.
  
## 🌐 API Hospedada

Para facilitar os testes, a API está hospedada no seguinte endereço:  
🔗 **[https://itaas.italodouglas.dev](https://itaas.italodouglas.dev)**

---

## 🚀 Como Executar

1. Clone o repositório:
   ```sh
   git clone https://github.com/italodouglasdev/iTaaS.git
   cd iTaaS
   ```
2. Configure a conexão com o banco no `appsettings.json`:
   ```json
   {
      "ConnectionStrings": {
         "DefaultConnection": "Initial Catalog=BANCO; User ID=USUARIO; Password=SENHA; Data Source=SERVIDOR; Encrypt=False;"
      }
   }
   ```
3. Execute a aplicação:
   ```sh
   dotnet run
   ```
4. Acesse a API via Swagger:
   🔗 **http://localhost:{porta}**

## 📌 Endpoints da API

A API disponibiliza os seguintes endpoints principais:

### Logs

- **POST /api/Log/TranformarLogUrl**
  - **Função**: Importa e transforma um log a partir de uma URL.
  - **Tipo HTTP**: POST
  - **Entrada**: Parâmetros `url` (string) e `tipoRetornoLog` (enum: 0 - ARQUIVO, 1 - PATCH, 2 - JSON).
  - **Saída**: Objeto `LogDto` contendo os dados do log transformado.

- **POST /api/Log/TranformarLogId**
  - **Função**: Importa e transforma um log a partir de um identificador.
  - **Tipo HTTP**: POST
  - **Entrada**: Parâmetros `id` (int) e `tipoRetornoLog` (enum: 0 - ARQUIVO, 1 - PATCH, 2 - JSON).
  - **Saída**: Objeto `LogDto` contendo os dados do log transformado.

- **GET /api/Log/BuscarSalvos**
  - **Função**: Busca logs salvos com base em diversos filtros.
  - **Tipo HTTP**: GET
  - **Parâmetros de entrada**:
    - `dataHoraRecebimentoInicio` (string): Data e hora inicial do recebimento do log.
    - `dataHoraRecebimentoFim` (string): Data e hora final do recebimento do log.
    - `metodoHttp` (string): Método HTTP utilizado na requisição (GET, POST, etc.).
    - `codigoStatus` (int): Código de status HTTP retornado.
    - `caminhoUrl` (string): URL acessada na requisição.
    - `tempoRespostaInicial` (double): Tempo mínimo de resposta da requisição (ms).
    - `tempoRespostaFinal` (double): Tempo máximo de resposta da requisição (ms).
    - `tamanhoRespostaInicial` (int): Tamanho mínimo da resposta em bytes.
    - `tamanhoRespostaFinal` (int): Tamanho máximo da resposta em bytes.
    - `cashStatus` (string): Status do cache utilizado na requisição.
    - `tipoRetornoLog` (enum: 0 - ARQUIVO, 1 - PATCH, 2 - JSON): Tipo do retorno do log.
  - **Saída**: Lista de objetos `LogDto` com os logs filtrados.

- **GET /api/Log/BuscarTransformados**
  - **Função**: Busca logs transformados com base em diversos filtros.
  - **Tipo HTTP**: GET
  - **Parâmetros de entrada**:
    - `dataHoraRecebimentoInicio` (string): Data e hora inicial do recebimento do log.
    - `dataHoraRecebimentoFim` (string): Data e hora final do recebimento do log.
    - `metodoHttp` (string): Método HTTP utilizado na requisição (GET, POST, etc.).
    - `codigoStatus` (int): Código de status HTTP retornado.
    - `caminhoUrl` (string): URL acessada na requisição.
    - `tempoRespostaInicial` (double): Tempo mínimo de resposta da requisição (ms).
    - `tempoRespostaFinal` (double): Tempo máximo de resposta da requisição (ms).
    - `tamanhoRespostaInicial` (int): Tamanho mínimo da resposta em bytes.
    - `tamanhoRespostaFinal` (int): Tamanho máximo da resposta em bytes.
    - `cashStatus` (string): Status do cache utilizado na requisição.
    - `tipoRetornoLog` (enum: 0 - ARQUIVO, 1 - PATCH, 2 - JSON): Tipo do retorno do log.
  - **Saída**: Lista de objetos `LogDto` com os logs transformados filtrados.

- **GET /api/Log/BuscarSalvoId/{identificador}**
  - **Função**: Busca um log salvo a partir do identificador.
  - **Tipo HTTP**: GET
  - **Entrada**: Parâmetro `identificador` do log (int).
  - **Saída**: Objeto `LogDto`.

- **GET /api/Log/BuscarTransformadoId/{identificador}**
  - **Função**: Busca um log transformado a partir do identificador.
  - **Tipo HTTP**: GET
  - **Entrada**: Parâmetro `identificador` do log (int).
  - **Saída**: Objeto `LogDto`.

- **POST /api/Log/Criar**
  - **Função**: Cria um novo log no sistema.
  - **Tipo HTTP**: POST
  - **Entrada**: Objeto JSON `LogDto`.
  - **Saída**: Objeto `LogDto` criado.

- **PUT /api/Log/Salvar**
  - **Função**: Atualiza um log existente.
  - **Tipo HTTP**: PUT
  - **Entrada**: Objeto JSON `LogDto` atualizado.
  - **Saída**: Objeto `LogDto` atualizado.

- **DELETE /api/Log/Deletar/{id}**
  - **Função**: Deleta um log do sistema pelo identificador.
  - **Tipo HTTP**: DELETE
  - **Entrada**: Parâmetro `id` (int).
  - **Saída**: Confirmação da exclusão.

---

## 🧪 Testes Unitários
Os testes unitários foram implementados para garantir a conversão correta dos logs e seguem boas práticas como:

- Princípios SOLID
- Mocks para serviços externos
- Testes de integração e unitários separados

Para rodar os testes, execute:
```sh
dotnet test
```

## 🛠 Tecnologias Utilizadas

- Visual Studio 2022
- .NET Core 2.1
- Entity Framework Core
- SQL Server
- Swagger
- Docker
- xUnit