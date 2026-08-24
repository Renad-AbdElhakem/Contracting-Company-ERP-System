using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Enum
{
    public enum ProjectStatus
    {
        Planned = 1,
        InProgress = 2,
        OnHold = 3,
        Completed = 4,
        Closed = 5,
        Cancelled = 6
    }
}
