using System;
using static PampaDevs.Utils.Helpers.IdHelper;

namespace PampaDevs.Bus
{
    public interface IMessage
    {
        Guid Id { get; }
        string MessageType { get; }        
    }

    public abstract class Message : IMessage
    {
        protected Message()
        {
            Id = NewId();
            MessageType = GetType().Name;            
        }
        public string MessageType { get; }
        public Guid Id { get; }
    }
}