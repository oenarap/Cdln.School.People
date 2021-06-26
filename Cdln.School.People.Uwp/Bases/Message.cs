using System;
using Apps.Communication.Core;

namespace Cdln.School.People.Uwp
{
    public abstract class Message<T> : Message
    {
        public T Data { get; }

        public Message(Guid id, T data)
            : base(id) { Data = data; }
    }

    public abstract class Message : IMessage
    {
        public Guid Id { get; }

        public Message(Guid id) { Id = id; }
    }
}
