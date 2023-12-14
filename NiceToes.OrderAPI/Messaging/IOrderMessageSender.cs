namespace NiceToes.OrderAPI.Messaging
{
    public interface IOrderMessageSender
    {
        void SendMessage(Object message, string exchangeName);
    }
}
