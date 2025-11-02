## How to run (simple)
```bash
dotnet restore
dotnet build
dotnet run --project src/Fortytwo.PracticalTest.Api/Fortytwo.PracticalTest.Api.csproj
```

Open `/swagger`, click **Authorize**, paste `Bearer <token>` you get from:
```
POST /auth/login
{ "username": "admin", "password": "password" }
```

## Endpoints
- `POST /auth/login` → returns JWT (admin/password)
- `POST /todos` → create (in-memory)
- `GET /todos` → list (in-memory)
- `GET /todos/{id}` → returns todo and `externalTitle` from jsonplaceholder

## Docker
```
docker build -t fortytwo-practical-test .
docker run -p 8080:8080 fortytwo-practical-test
```
Then open `http://localhost:8080/swagger`.
