﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Department:EntityBase
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
