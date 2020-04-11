using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugtracker.Domain
{
    public class Audit
    {
        public string Id { get; set; }

        public string Property { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }
    }
}
