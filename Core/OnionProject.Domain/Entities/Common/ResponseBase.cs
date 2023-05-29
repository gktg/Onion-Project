using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionProject.Domain.Entities.Common
{
    public class ResponseBase
    {
        public bool HasError { get; set; }

        public List<string> Error { get; set; }

    }
}
