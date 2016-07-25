#load "..\FeedbackReceiver\Types.csx"
#r "Twilio.Api"

using System;
using System.Threading.Tasks;
using Twilio;

public static void Run(FeedbackSubmission feedbackItem, out SMSMessage notification, TraceWriter log)
{
    string expression = GetFeedbackExpression(feedbackItem.SentimentScore);
    
    notification = new SMSMessage
    {
        To = feedbackItem.PhoneNumber,
        Body = $"Your feedback was received and assiged a score of '{expression}'\r\nLearn more at https://aka.ms/azurefunctionstr23"
    };
}

static string[] expressions = new[] { ">:(", ":(", ":|", ":)", ":D" };
public static string GetFeedbackExpression(float score)
{
    int index = (int)(score / 0.2);
    return expressions[index];
}