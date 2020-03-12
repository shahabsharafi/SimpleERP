﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Document.API.Infrastructure.Data
{
    public class Type
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool Readonly { get; set; }
        public bool Hidden { get; set; }
    }
}
