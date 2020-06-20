using MediatR;
using System;
using static PampaDevs.Utils.Helpers.DateTimeHelper;

namespace PampaDevs.BuildingBlocks.Bus.InProc
{
    public abstract class Event : Message, IEvent, INotification
    {
        public int EventVersion { get; protected set; }

        public DateTime OcurredOn { get; }
        public Event() : base()
        {
            EventVersion = 1;
            OcurredOn = NewDateTime();
        }
    }
}
