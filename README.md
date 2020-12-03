# Minimal ML.NET ASP.NET Web API

Minimal ML .NET ASP.NET Web API to predict sentiment.

- `main` branch contains ASP.NET Core 3.1 version
- `net50` branch contains .NET 5 version.

## Run the application

To use it, run the application and make an HTTP `POST` request to `http(s)//localhost:PORT/predict`. Make sure to replace the port with the port your web service is listening on.

### Sample request:

```json
{
	"SentimentText":"This was a very bad steak"
}
```

### Sample response: 

```json
{
  "Prediction": false,
  "Probability": 0.5,
  "Score": 0
}
```

## Docker

To use with docker:

### Build image

```bash
docker build -t minimalmlnetwebapi:latest -f Dockerfile .
```

### Run app from image

```bash
docker run --rm -d  -p 5000:80/tcp minimalmlnetwebapi:latest
```