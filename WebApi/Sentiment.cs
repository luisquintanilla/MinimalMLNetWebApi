using Microsoft.ML.Data;

namespace WebApi
{
    public class Sentiment
    {
        public class Input
        {
            public string SentimentText;

            [ColumnName("Label")]
            public bool Sentiment;
        }

        public class Output
        {
            [ColumnName("PredictedLabel")]
            public bool Prediction { get; set; }

            public float Probability { get; set; }

            public float Score { get; set; }
        }
    }
}