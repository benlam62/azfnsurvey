#load "types.csx"

using System.Net;

public static void Run(FeedbackSubmission req, out FeedbackSubmission outputSbMsg, TraceWriter log)
{
    outputSbMsg = req;
}