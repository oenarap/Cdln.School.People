using System;
using Apps.Communication.Core;

namespace Cdln.School.People.Uwp
{
    public abstract class MessageCommand<T> : ICommand
    {
        public Guid Id { get; }
        public T Data { get; }

        public MessageCommand(Guid id, T data)
        {
            Id = id;
            Data = data;
        }
    }
}
