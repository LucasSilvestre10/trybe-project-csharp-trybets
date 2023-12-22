# TryBets


### Requisitos Implementados

#### 1. **TryBets.Users:**
   - Microsserviço responsável pelo cadastro e login de usuários.
   - Implementação das rotas `POST /user/signup` e `POST /user/login`.
   - Utilização das controllers, DTOs, services e repositories do serviço monolítico.

   Exemplo de Requisição (Cadastro):
   ```json
   {
      "Name": "João Silva",
      "Email": "joao.silva@example.com",
      "Password": "123456"
   }
   ```
   Exemplo de Resposta (Sucesso):
   ```json
   {
      "token": "eyJhbG..."
   }
   ```
   Exemplo de Resposta (Erro - E-mail já cadastrado):
   ```json
   {
      "message": "E-mail already used"
   }
   ```

   Exemplo de Requisição (Login):
   ```json
   {
      "Email": "joao.silva@example.com",
      "Password": "123456"
   }
   ```
   Exemplo de Resposta (Sucesso):
   ```json
   {
      "token": "eyJhbG..."
   }
   ```
   Exemplo de Resposta (Erro - Autenticação falhou):
   ```json
   {
      "message": "Authentication failed"
   }
   ```

#### 2. **TryBets.Matches:**
   - Microsserviço para visualização de times e partidas.
   - Implementação das rotas `GET /team` e `GET /match/{finished}`.
   - Reutilização de controllers, DTOs e repositories do serviço monolítico.

   Exemplo de Requisição (Visualizar Times):
   ```json
   GET /team
   ```
   Exemplo de Resposta:
   ```json
   [
      {
         "TeamId": 1,
         "TeamName": "Sharks"
      },
      {
         "TeamId": 2,
         "TeamName": "Bulls"
      },
      /* ... */
   ]
   ```

   Exemplo de Requisição (Visualizar Partidas Finalizadas):
   ```json
   GET /match/true
   ```
   Exemplo de Resposta:
   ```json
   [
      {
         "MatchId": 1,
         "MatchDate": "2023-07-23T15:00:00",
         "TeamAName": "Sharks",
         "TeamBName": "Bulls",
         "MatchFinished": true,
         "MatchWinnerId": 1
      },
      /* ... */
   ]
   ```

#### 3. **TryBets.Bets:**
   - Microsserviço para cadastro e visualização de apostas.
   - Implementação das rotas `POST /bet` e `GET /bet/{BetId}`.
   - Utilização das controllers, DTOs e repositories do serviço monolítico.
   - Proteção das rotas com a política de acesso `Client`.

   Exemplo de Requisição (Cadastrar Aposta):
   ```json
   {
      "MatchId": 5,
      "TeamId": 2,
      "BetValue": 550.65
   }
   ```
   Exemplo de Resposta (Sucesso):
   ```json
   {
      "betId": 1,
      "matchId": 5,
      "teamId": 2,
      "betValue": 550.65,
      "matchDate": "2024-03-15T14:00:00",
      "teamName": "Eagles",
      "email": "joao.silva@example.com"
   }
   ```
   Exemplo de Resposta (Erro - Token não informado ou inválido):
   ```json
   {
      "message": "Unauthorized"
   }
   ```

#### 4. **TryBets.Odds:**
   - Novo microsserviço para atualização das odds de cada partida.
   - Implementação da rota `PATCH /odd/{matchId}/{TeamId}/{BetValue}`.
   - Refatoração do microsserviço `TryBets.Bets` para integrar `TryBets.Odds`.

   Exemplo de Requisição (Atualizar Odds):
   ```json
   PATCH /odd/1/2/550.65
   ```
   Exemplo de Resposta (Sucesso):
   ```json
   {
      "matchId": 1,
      "matchDate": "2024-03-17T14:00:00",
      "matchTeamAId": 5,
      "matchTeamBId": 6,
      "matchTeamAValue": 300.00,
      "matchTeamBValue": 1501.50,
      "matchFinished": false,
      "matchWinnerId": null,
      "matchTeamA": null,
      "matchTeamB": null,
      "bets": null
   }
   ```
   Exemplo de Resposta (Erro - Qualquer tipo de erro que impeça a atualização):
   ```json
   {
      "message": "Error updating odds"
   }
   ```

#### 5. **Dockerfiles:**
   - Desenvolvimento dos Dockerfiles para cada microsserviço capaz de criar um container da API.

   Exemplo de Comando para Construir o Container:
   ```shell
   docker build -t trybets-users:latest .
   ```
   Exemplo de Comando para Executar o Container:
   ```shell
   docker run -p 5501:5501 -d trybets-users
   ```

---

### Habilidades Técnicas

- **Linguagens:**
  - C#
  - Markdown

- **Tecnologias:**
  - ASP.NET Core
  - Docker
  - Entity Framework
  - SQL Server
