#load "types.csx"
#r "Newtonsoft.Json"

using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

public static async Task Run(HttpRequestMessage req, IAsyncCollector<FeedbackSubmission> outputQueueItem, TraceWriter log)
{
    if (!req.Content.IsFormData())
    {
        req.CreateResponse(System.Net.HttpStatusCode.UnsupportedMediaType); 
    }
    
    var smsProperties = await req.Content.ReadAsFormDataAsync();
        
    FeedbackSubmission submission = new FeedbackSubmission 
    {
        PhoneNumber = smsProperties["From"],
        Text = smsProperties["Body"],
    };
    
    await outputQueueItem.AddAsync(submission);
}