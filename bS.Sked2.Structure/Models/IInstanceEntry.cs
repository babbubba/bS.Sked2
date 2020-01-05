using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Models
{
    public interface IInstanceEntry
    {
        Guid Id { get; set; }
        IList<IMessageEntry> Messages { get; set; }
        DateTime? BeginTime { get; set; }
        DateTime? EndTime { get; set; }
        bool IsPaused { get; set; }
    }
}
