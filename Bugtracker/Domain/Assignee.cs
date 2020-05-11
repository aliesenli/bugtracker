using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugtracker.Domain
{
    public class Assignee
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
    }
}
