using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.Types
{
    public class ServiceMessage
    {
        public bool IsSucceed { get; set; }

        public string Message { get; set; }

    }

    public class ServiceMessage<T>
    {
        public string Message { get; set; }

        public bool IsSucceed { get; set; }

        public T? Data { get; set; }

    }
}


