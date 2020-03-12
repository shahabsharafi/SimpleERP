﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleERP.Infrastructure.Commons;
using SimpleERP.Document.API.Infrastructure.Data;

namespace SimpleERP.Document.API.Infrastructure.Contracts
{
    public interface IDocumentInfoRepository :  IRepository<DocumentInfo> 
    {
    }
}