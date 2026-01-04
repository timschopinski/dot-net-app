# Filmweb API - CRUD z Autoryzacją JWT

REST API do zarządzania filmami i reżyserami z pełną autoryzacją JWT i dokumentacją Swagger.

## Uruchomienie

1. **Uruchom aplikację**:
```bash
docker-compose up --build
```

2**Otwórz Swagger UI**:
```
http://localhost:8080
```

Swagger UI zostanie automatycznie otwarty na głównej stronie.

## Przykładowe użycie

### 1. Rejestracja
W Swagger UI, rozwiń endpoint `POST /api/auth/register` i wykonaj:
```json
{
  "username": "tim",
  "email": "tim@example.com",
  "password": "password"
}
```

Otrzymasz JWT w odpowiedzi:
```json
{
  "token": "jakis-token...",
  "username": "tim"
}
```

### 2. Autoryzacja w Swaggerze
1. Kliknij przycisk **"Authorize"** w prawym górnym rogu Swagger UI
2. Wpisz: `Bearer <twoj-token>` (zamień `<twoj-token>` na otrzymany token)
3. Kliknij "Authorize"
4. Teraz możesz wykonywać operacje wymagające autoryzacji

### 3. Dodaj Reżysera
```json
{
  "firstName": "Christopher",
  "lastName": "Nolan",
  "birthDate": "1970-07-30",
  "nationality": "British"
}
```

### 4. Dodaj Film
```json
{
  "title": "Inception",
  "description": "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO, but his tragic past may doom the project and his team to disaster.",
  "releaseYear": 2010,
  "genre": "Sci-Fi",
  "directorId": 1
}
```

