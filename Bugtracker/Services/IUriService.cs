using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugtracker.Services
{
    public interface IUriService
    {
        Uri GetPostUri(string ticketId);

        Uri GetProjectUri(string projectId);
    }
}
