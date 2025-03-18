# 📌 Conversor de Logs - MINHA CDN para Agora

## 📖 Sobre o Projeto
Arquivos de log podem revelar muito sobre o comportamento de um sistema em um ambiente de produção. A extração de dados desses arquivos auxilia na tomada de decisões para o planejamento de negócios e desenvolvimento.

A **iTaaS Solution** é uma empresa focada em entrega de conteúdo e enfrentava altos custos com CDN (Content Delivery Network), o que impactava seus clientes e seus lucros. Para resolver isso, firmaram um contrato com a empresa **"MINHA CDN"**.

O desafio deste projeto é converter os logs gerados no formato **"MINHA CDN"** para o formato **"Agora"**, que é utilizado pelo sistema da empresa para relatórios de faturamento.

## 📂 Exemplo de Logs
### 📥 Entrada (Formato "MINHA CDN")
```txt
312|200|HIT|"GET /robots.txt HTTP/1.1"|100.2
101|200|MISS|"POST /myImages HTTP/1.1"|319.4
199|404|MISS|"GET /not-found HTTP/1.1"|142.9
312|200|INVALIDATE|"GET /robots.txt HTTP/1.1"|245.1
```

### 📤 Saída (Formato "Agora")
```txt
#Version: 1.0
#Date: 15/12/2017 23:01:06
#Fields: provider http-method status-code uri-path time-taken response-size cache-status
"MINHA CDN" GET 200 /robots.txt 100 312 HIT
"MINHA CDN" POST 200 /myImages 319 101 MISS
"MINHA CDN" GET 404 /not-found 143 199 MISS
"MINHA CDN" GET 200 /robots.txt 245 312 REFRESH_HIT
```

## 🔗 Endpoints da API
A API Restful foi desenvolvida para realizar as seguintes operações:

1️⃣ **Transformar um Log de Entrada no formato "MINHA CDN" para "Agora"**
   - O log pode vir de uma **URL** ou ser um **identificador de um log salvo**
   - O resultado pode ser salvo em um arquivo no servidor ou retornado diretamente na resposta

2️⃣ **Buscar Logs Salvos**

3️⃣ **Buscar Logs Transformados no Backend**
   - Retorna tanto o log original no formato "MINHA CDN" quanto a saída convertida no formato "Agora"

4️⃣ **Buscar Logs Salvos por Identificador**

5️⃣ **Buscar Logs Transformados por Identificador**

6️⃣ **Salvar Logs**

## 🛠️ Tecnologias Utilizadas
- **.NET Core 2.1**
- **Entity Framework Core**
- **SQL Server**

## 🚀 Como Executar o Projeto
1️⃣ **Clone o repositório**
```bash
git clone https://github.com/seuusuario/seuprojeto.git
```

2️⃣ **Acesse o diretório do projeto**
```bash
cd seuprojeto
```

3️⃣ **Configure a string de conexão com o SQL Server** no `appsettings.json`

4️⃣ **Execute a aplicação**
```bash
dotnet run
```

## 📌 Testes
O projeto inclui **testes unitários** utilizando **Mocks** para garantir a qualidade do código. Os testes estão em um projeto separado e podem ser executados com:
```bash
dotnet test
```

## 🔗 Link para Arquivo de Teste
Para testar, use o arquivo de exemplo disponível:
[Baixar Arquivo de Log](https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt)

---

✉️ **Contato:** Se tiver dúvidas ou sugestões, sinta-se à vontade para abrir uma **issue** ou entrar em contato. 😊

