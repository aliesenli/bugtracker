using System.Collections.Generic;

namespace Bugtracker.Contracts.Responses
{
    public class Responses<T>
    {
        //public Responses() { }

        public Responses(IEnumerable<T> responses)
        {
            Data = responses;
        }

        public IEnumerable<T> Data { get; set; }
    }
}
