# fauna-aspnet-sample
A lightweight ASP.NET Core Web API sample using FaunaDB under the hood. This project was created to try it out and implement a basic CRUD controller in C#.

## References

- [FaunaDB](https://fauna.com/)
- [FaunaDB C# Driver docs](https://docs.fauna.com/fauna/current/drivers/csharp)
- [FaunaDB basic CRUD docs](https://docs.fauna.com/fauna/current/tutorials/crud?lang=csharp)

## Next steps

This project is still in an very early stage and most likely will be improved once I've more time to dive deeper into FaunaDB and [FQL](https://docs.fauna.com/fauna/current/start/fql_for_sql_users).

## Usage

### Prerequisites

- FaunaDB account with a database and a collection named `artist` with and index for Artist name called `name-index`.
- Server Key for the database to use for data queries.

### Docker

The application can be built with `docker build` command:

```
docker build --no-cache --rm  -t local/fauna-asp-net:v1 .
```

After that you can run it with `docker run`:

```
docker run --rm -p 8000:80 -e "Fauna:Secret=<SERVER_KEY>" local/fauna-asp-net:v1
```

Navigating to `http://localhost:8000` should open up swagger with all the available endpoints.

Alternatively it can be fired up using `docker-compose up` command after setting the server key in `docker-compose.yaml`:

```
docker-compose up
```