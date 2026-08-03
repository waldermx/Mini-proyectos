// using Chat.Conversation.Domain;

// public class Conversation
// {
//     public Guid Id {get; init;}
//     private readonly List<Message> _messagesList = [];
//     public IReadOnlyCollection<Message> Messages => _messagesList.AsReadOnly();
//     public Guid UsuarioAId { get; init; }
//     public Guid UsuarioBId { get; init; }
//     private Conversation(Guid usuarioA, Guid usuarioB)
//     {
//         Id = Guid.NewGuid();

//         if (usuarioA.CompareTo(usuarioB) < 0)
//         {
            
//             UsuarioAId = usuarioA;
//             UsuarioBId = usuarioB;
//         }
//         else
//         {
//             UsuarioAId = usuarioB;
//             UsuarioBId = usuarioA;
//         }
//     }

//     public static Conversation CreateConversation(Guid usuarioAId, Guid usuarioBId, bool sonAmigos)
//     {
//         if (!sonAmigos)
//         {
//             throw new InvalidOperationException("No se puede iniciar una conversación entre personas que no son amigos");
//         }

//         if (usuarioAId == usuarioBId)
//         {
//             throw new InvalidOperationException("No se puede crear una conversación consigo mismo");
//         }
//     }

//     public void AddMessage(Guid emisorId, string texto)
//     {
//         _messagesList.Add(new Message(emisorId, texto));
//     }

// }