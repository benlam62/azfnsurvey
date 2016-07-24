#load "..\FeedbackProcessor\Types.csx"
#r "Twilio.Api"

using System;
using System.Threading.Tasks;
using Twilio;

public static void Run(FeedbackSubmission feedback, out SMSMessage notification, TraceWriter log)
{
    notification = new SMSMessage
    {
        To = feedback.PhoneNumber,
        Body = feedback.Name
    };
}