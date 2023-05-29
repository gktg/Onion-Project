using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionProject.Domain.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; } = DateTime.Now;
        public DateTime UpdateTime { get; } = DateTime.Now;
        public DateTime DeleteTime { get; set; }
    }
}
