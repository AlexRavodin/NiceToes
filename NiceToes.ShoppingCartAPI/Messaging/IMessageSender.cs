namespace NiceToes.ShoppingCartAPI.Messaging
{
    public interface IMessageSender
    {
        void SendMessage(object message, string queueName);
    }
}
