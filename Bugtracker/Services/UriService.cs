using System;

namespace Bugtracker.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetTicketUri(string ticketId)
        {
            return new Uri(_baseUri + "api/tickets/{postId}".Replace("{postId}", ticketId));
        }

        public Uri GetProjectUri(string projectId)
        {
            return new Uri(_baseUri + "api/projects/{projectId}".Replace("{projectId}", projectId));
        }
    }
}
