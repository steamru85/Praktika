﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StagingWizard.UIContracts
{
    public class AddServerContract: Server
    {
        public Guid StagingId { get; set; }
    }
}
