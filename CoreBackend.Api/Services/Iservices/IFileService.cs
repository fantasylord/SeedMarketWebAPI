﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackend.Api.Services.Iservices
{
    public interface IFileService
    {
         String PrintFileCreate( IFormFile file);

    }
}
