#load "..\FeedbackProcessor\Types.csx"

using System;
using System.Threading.Tasks;

public static void Run(FeedbackSubmission mySbMsg, out FeedbackSubmission outputDocument, TraceWriter log)
{
    outputDocument = mySbMsg;
}