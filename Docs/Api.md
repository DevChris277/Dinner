# Buber Dinner API

- [Buber Dinner API](#buber-dinner-api)
  - [Auth](#auth)
    - [Register](#register)
      - [Register Request](#register-request)
      - [Register Response](#register-response)
    - [Login](#login)
      - [Login Request](#login-request)
      - [Login Response](#login-response)

## Auth

### Register

```js
POST {{host}}/auth/register
```

#### Register Request

```json
{
    "firstName": "Chris",
    "lastName": "Needhamn",
    "email": "chris@needham.com",
    "password": "P@ssword!"
}
```

#### Register Response

```js
200 OK
```

```json
{
  "id": "d89c2d9a-eb3e-4075-95ff-b920b55aa104",
  "firstName": "Chris",
  "lastName": "Needham",
  "email": "chris@needham.com",
  "token": "eyJhb..z9dqcnXoY"
}
```

### Login

```js
POST {{host}}/auth/login
```

#### Login Request

```json
{
    "email": "chris@needham.com",
    "password": "P@ssword1"
}
```

```js
200 OK
```

#### Login Response

```json
{
  "id": "d89c2d9a-eb3e-4075-95ff-b920b55aa104",
  "firstName": "Chris",
  "lastName": "Needham",
  "email": "amichai@mantinband.com",
  "token": "eyJhb..hbbQ"
}
```