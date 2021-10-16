using MediatR;
using System;

namespace ReclameAquiDoTrader.Business.Core.Messages
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event(string aggregatedId)
        {
            AggregateId = aggregatedId;
            Timestamp = DateTime.Now;
        }
    }
}
