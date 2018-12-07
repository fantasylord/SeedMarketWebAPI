using CoreBackend.Api.Services.Iservices;
using CoreBackend.Api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static CoreBackend.Api.Utils.FileStreamPandO;

namespace CoreBackend.Api.Services
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
      
        string[] pictureFormatArray = { "png", "jpg", "jpeg", "bmp", "gif", "ico",
                                        "PNG","JPG","JPEG","BMP","GIF","ICO"};
        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 返回存储路径 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="file"></param>
        /// <returns>数据库存储路径！！！ </returns>
        public string PrintFileCreate( IFormFile file)
        {
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            string filePath = ServiceConfigs.FileUpDirectory;
            FilesPrint fp = new FilesPrint();
            string suffix = fileName.Split('.')[1];
            if ((!pictureFormatArray.Contains(suffix)))
            {
                _logger.LogWarning("+文件类型错误+");
                return null;
            }
            filePath += @"\" + System.DateTime.Now.Year.ToString() + @"\" + System.DateTime.Now.Month.ToString() + @"\" + System.DateTime.Now.Day.ToString();//文件夹
            UnixStamp ustamp = new UnixStamp();
            if (!(Directory.Exists(filePath)))
            {
                Directory.CreateDirectory(filePath);

            }
            fileName = @"\" + ustamp.DateTimeToStamp(System.DateTime.Now) + "." + suffix;
            string fileFullName = filePath + fileName;
            try
            {
                using (FileStream fs = System.IO.File.Create(fileFullName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            catch (Exception e)
            {
                string ss = e.ToString();
                _logger.LogError($"+存储异常+{ss}");
                return null;
            }
            FileStreamPandO.FilesPrint f = new FilesPrint();
            return  f.SaveSRC(fileFullName) ;
 
        }
    }
}
