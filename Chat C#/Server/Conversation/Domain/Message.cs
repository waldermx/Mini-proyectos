namespace Chat.Conversation.Domain;


public record class Message
{
    public Guid Id {get; init;}= Guid.NewGuid();
    public DateTime CreatedAt {get; init;}= DateTime.UtcNow;

    public string MessageContent {get; init;}
    public Message(Guid senderId, string text)
    {

        ArgumentException.ThrowIfNullOrWhiteSpace(text);
        MessageContent = text;
    

    }
}