﻿using OnionProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionProject.Domain.Entities
{
    public class Session : ResponseBase
    {
        public Guid SessionId { get; set; }
    }
}