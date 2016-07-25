#load "..\FeedbackReceiver\Types.csx"
#r "Newtonsoft.Json"

using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

public static async Task Run(FeedbackSubmission feedbackItem, IAsyncCollector<FeedbackSubmission> outputDocument,
    IAsyncCollector<FeedbackSubmission> notificationQueue, TraceWriter log)
{
    feedbackItem.SentimentScore = await GetSentimentScore(feedbackItem.Text);
    
    log.Info($"Processing function received feedback with score '{feedbackItem.SentimentScore}'");
    
    await outputDocument.AddAsync(feedbackItem);
    await notificationQueue.AddAsync(feedbackItem);
}

public async static Task<float> GetSentimentScore(string text)
{
    var document = JsonConvert.SerializeObject(new { documents = new[] { new { language = "en", id = 1, text = text } } });
    string sentimentUri = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment";

    using (var client = new HttpClient())
    {
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Environment.GetEnvironmentVariable("TextAnalysisApiKey"));
        using (var response = await client.PostAsync(sentimentUri, new StringContent(document, Encoding.UTF8, "application/json")))
        {
            JObject result = await response.Content.ReadAsAsync<JObject>();
            return result.SelectToken("documents[0].score")?.Value<float>() ?? 0;
        }
    }
}