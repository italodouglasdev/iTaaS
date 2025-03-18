Aqui está o `README.md` atualizado com as informações sobre o Swagger e a hospedagem do projeto.  

```md
# Conversor de Logs - iTaaS Solution

## 📜 Descrição

Arquivos de log podem revelar muito sobre o comportamento de um sistema em um ambiente de produção. A extração de dados desses arquivos auxilia na tomada de decisões para o planejamento de negócios e desenvolvimento.

A **iTaaS Solution** é uma empresa focada em entrega de conteúdo, e um dos seus maiores desafios de negócio era o **custo com CDN (Content Delivery Network)**. Custos altos aumentam o preço final para os clientes, reduzem lucros e dificultam a entrada em mercados menores.

Após uma pesquisa, a empresa firmou contrato com a **MINHA CDN**, que utiliza um formato de log diferente do atual sistema de relatórios de faturamento, chamado **"Agora"**. O objetivo do projeto é desenvolver uma **API REST** que converta os arquivos de log do formato **MINHA CDN** para o formato **Agora**.

---

## 🔧 Tecnologias Utilizadas

- **.NET Core 2.1**
- **Entity Framework Core**
- **SQL Server**
- **Swagger** (para facilitar os testes)
- **Docker** (opcional, para facilitar a execução)

---

## 🚀 Funcionalidades

A API possui os seguintes endpoints:

- **Transformação de Logs**
  - Entrada pode ser uma URL ou um identificador salvo no banco.
  - Saída pode ser salva em um arquivo ou retornada diretamente na resposta.

- **Gerenciamento de Logs**
  - Buscar logs salvos.
  - Buscar logs transformados no backend.
  - Buscar logs por identificador.
  - Salvar logs no banco de dados.

---

## 📂 Exemplo de Conversão

### 🔹 Log de Entrada - Formato MINHA CDN

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
         "DefaultConnection": "Server=localhost;Database=MinhaCDN;User Id=sa;Password=your_password;"
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

## 🧪 Testes Unitários

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
```

Esse `README.md` está mais profissional, organizado e contém todas as informações importantes. Se precisar de ajustes, só avisar! 🚀