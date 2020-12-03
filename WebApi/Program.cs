using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Configuration
            WebHost.CreateDefaultBuilder()
                .ConfigureServices(services => {
                    // Register prediction engine
                    services.AddPredictionEnginePool<Sentiment.Input,Sentiment.Output>()
                        .FromUri("https://github.com/dotnet/samples/raw/master/machine-learning/models/sentimentanalysis/sentiment_model.zip");
                })
                .Configure(options => {
                    options.UseRouting();
                    options.UseEndpoints(routes => {
                        // Define prediction endpoint
                        routes.MapPost("/predict", PredictHandler);
                    });
                })
                .Build()
                .Run();
        }

        static async Task PredictHandler(HttpContext http)
        {
            // Get PredictionEnginePool service
            var predEngine = http.RequestServices.GetRequiredService<PredictionEnginePool<Sentiment.Input, Sentiment.Output>>();

            // Deserialize HTTP request JSON body
            var body = http.Request.Body as Stream;
            var input = await JsonSerializer.DeserializeAsync<Sentiment.Input>(body);
                
            // Predict
            var prediction = predEngine.Predict(input);
                
            // Return prediction as response
            var response = JsonSerializer.Serialize<Sentiment.Output>(prediction);
            http.Response.ContentType = "application/json";
            await http.Response.WriteAsync(response);
        }
    }
}
