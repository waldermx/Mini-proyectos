namespace Chat.Conversation.Domain;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string UserName { get; private set; }

    public User(string name, string username)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "El nombre es obligatorio");
        }

        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentNullException(nameof(username), "El nombre de usuario no puede estar vacío.");
        }

        Name = name;
        UserName = username;

    }
}