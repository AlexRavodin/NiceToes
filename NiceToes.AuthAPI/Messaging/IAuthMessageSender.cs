namespace NiceToes.AuthAPI.Messaging
{
    public interface IAuthMessageSender
    {
        void SendMessage(Object message, string queueName);
    }
}
