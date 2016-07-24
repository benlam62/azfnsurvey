public class FeedbackSubmission
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public List<Answer> Answers { get; set; }
}

public class Answer
{
    public string QuestionId { get; set; }
    public string Value { get; set; }
}