# Conversor de Logs - iTaaS Solution

## 🌜 Descrição

Arquivos de log podem revelar muito sobre o comportamento de um sistema em um ambiente de produção. A extração de dados desses arquivos auxilia na tomada de decisões para o planejamento de negócios e desenvolvimento.

A **iTaaS Solution** é uma empresa focada em entrega de conteúdo, e um dos seus maiores desafios de negócio era o **custo com CDN (Content Delivery Network)**. Custos altos aumentam o preço final para os clientes, reduzem lucros e dificultam a entrada em mercados menores.

Após uma pesquisa, a empresa firmou contrato com a **MINHA CDN**, que utiliza um formato de log diferente do atual sistema de relatórios de faturamento, chamado **"Agora"**. O objetivo do projeto é desenvolver uma **API REST** que converta os arquivos de log do formato **MINHA CDN** para o formato **Agora**.

## 🔧 Tecnologias Utilizadas

- **Visual Studio 2022**
- **.NET Core 2.1**
- **Entity Framework Core**
- **SQL Server**
- **Swagger**
- **Docker**
- **xUnit**

---

## 🚀 Funcionalidades

A API possui os seguintes endpoints:

### 🔹 POST /api/Log/TranformarLogUrl

 - Descrição: Importa e transforma um log a partir de uma URL fornecida.
 - Retorno: Arquivo, JSON ou Patch (conforme o tipo solicitado)


### 🔹 POST /api/Log/TranformarLogId

 - Descrição: Importa e transforma um log a partir de um identificador fornecido.
 - Retorno: Arquivo, JSON ou Patch (conforme o tipo solicitado)


### 🔹 GET /api/Log/BuscarSalvos

 - Descrição: Busca logs salvos com filtros (data, status, tempo de resposta, etc.).
 - Retorno: Lista de logs salvos

### 🔹 GET /api/Log/BuscarTransformados

 - Descrição: Busca logs transformados com filtros semelhantes
 - Retorno: Lista de logs transformados

### 🔹 GET /api/Log/BuscarSalvoId/{identificador}

 - Descrição: Busca um log salvo por identificador
 - Retorno: Log salvo específico

### 🔹 GET /api/Log/BuscarTransformadoId/{identificador}

 - Descrição: Busca um log transformado por identificador
 - Retorno: Log transformado específico

### 🔹 POST /api/Log/Criar

 - Descrição: Cria um novo log.
 - Retorno: Confirmação da criação do log com os detalhes

### 🔹 PUT /api/Log/Salvar

 - Descrição: Atualiza um log
 - Retorno: Confirmação da criação do log com os detalhes

### 🔹 DELETE /api/Log/Deletar/{identificador}

 - Descrição: Deleta um log por por identificador
 - Retorno: Confirmação da criação do log com os detalhes


---

## 📂 Exemplo de Conversão

### 🔸 Log de Entrada - Formato MINHA CDN

```txt
312|200|HIT|"GET /robots.txt HTTP/1.1"|100.2
101|200|MISS|"POST /myImages HTTP/1.1"|319.4
199|404|MISS|"GET /not-found HTTP/1.1"|142.9
312|200|INVALIDATE|"GET /robots.txt HTTP/1.1"|245.1
```

### 🔸 Log Convertido - Formato Agora

```txt
#Version: 1.0
#Date: 15/12/2017 23:01:06
#Fields: provider http-method status-code uri-path time-taken response-size cache-status
"MINHA CDN" GET 200 /robots.txt 100 312 HIT
"MINHA CDN" POST 200 /myImages 319 101 MISS
"MINHA CDN" GET 404 /not-found 143 199 MISS
"MINHA CDN" GET 200 /robots.txt 245 312 REFRESH_HIT
```

---

## 🛠 Como Executar

1. **Clone o repositório**  
   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio
   ```

2. **Configure a conexão com o banco no `appsettings.json`**  
   ```json
   {
      "ConnectionStrings": {
         "DefaultConnection": "Initial Catalog=BANCO; User ID=USUARIO; Password=SENHA; Data Source=SERVIDOR; Encrypt=False;"
      }
   }
   ```

3. **Execute a aplicação**  
   ```bash
   dotnet run
   ```

4. **Acesse a API via Swagger**  
   - **URL:** [`http://localhost:5000/swagger`](http://localhost:5000/swagger)

---

## 🌐 API Hospedada

Para facilitar os testes, a API está hospedada no seguinte endereço:  
🔗 **[https://itaas.italodouglas.dev](https://itaas.italodouglas.dev)**

---

## 🥾 Testes Unitários

Os testes unitários foram implementados para garantir a conversão correta dos logs e seguem boas práticas como:

- **Princípios SOLID**
- **Mocks para serviços externos**
- **Testes de integração e unitários separados**

Para rodar os testes, execute:

```bash
dotnet test
```

---

## 📌 Considerações Finais

Este projeto não apenas resolve um problema técnico, mas também ajuda a iTaaS Solution a reduzir custos operacionais e melhorar a eficiência de sua análise de dados. 🚀

