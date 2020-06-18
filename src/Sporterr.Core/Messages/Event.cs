using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.Messages
{
    public abstract class Event : Message, INotification
    {
    }
}
