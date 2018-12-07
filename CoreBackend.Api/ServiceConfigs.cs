using CoreBackend.Api.Dtos;
using CoreBackend.Api.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackend.Api
{
    public class ServiceConfigs
    {
        /// <summary>
        /// img存放目录
        /// </summary>
        public static readonly string FileUpDirectory = Path.GetDirectoryName((new Program()).GetType().Assembly.Location) + @"\imgs";

    }
 
}
