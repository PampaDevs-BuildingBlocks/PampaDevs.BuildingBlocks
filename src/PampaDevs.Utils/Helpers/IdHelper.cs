using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PampaDevs.Utils.Helpers
{
    public static class IdHelper
    {
        [DebuggerStepThrough]
        public static Guid NewId(string guid = "")
        {
            return string.IsNullOrEmpty(guid) ? Guid.NewGuid() : new Guid(guid);
        }

        [DebuggerStepThrough]
        public static Guid EmptyId()
        {
            return Guid.Empty;
        }
    }
}
