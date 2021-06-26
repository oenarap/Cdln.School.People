using Apps.Communication.Core;

namespace Cdln.School.People.Uwp
{
    public interface IMessageHub
    {
        void Dispatch<TMessage>(TMessage command) where TMessage : IMessage;
        void RegisterHandler<THandler, TMessage>(THandler handler)
            where THandler : IHandle<TMessage>
            where TMessage : IMessage;
        void UnregisterHandler<THandler, TMessage>(THandler handler)
            where THandler : IHandle<TMessage>
            where TMessage : IMessage;
    }
}