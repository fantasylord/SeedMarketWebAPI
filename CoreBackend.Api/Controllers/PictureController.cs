using AutoMapper;
using CoreBackend.Api.Repositories.IRepositories;
using CoreBackend.Api.Services.Iservices;
using CoreBackend.Api.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static CoreBackend.Api.Utils.FileStreamPandO;

namespace CoreBackend.Api.Controllers
{
    /// <summary>
    /// 文件上传
    /// </summary>
    [Route("api/[controller]")]
    public class PictureController:Controller
    {
        private IHostingEnvironment hostingEnv;
        private readonly ILogger<PictureController> _logger;
        private readonly IMailService _mailService;
        private readonly ISeedRepository _productRepository;
        private readonly IMapper _Mapper;
        string[] pictureFormatArray = { "png", "jpg", "jpeg", "bmp", "gif", "ico",
                                        "PNG","JPG","JPEG","BMP","GIF","ICO"};
        public PictureController(IHostingEnvironment env,
                                ILogger<PictureController> logger,
                                IMailService mailService,
                                ISeedRepository seedRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _productRepository = seedRepository;
            this.hostingEnv = env;
        }
        /// <summary>
        /// 多个文件上传
        /// </summary>
        /// <param name="type"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json", "multipart/form-data")]//此处为新增
        [HttpPost]
        public IActionResult Post(string type, IFormCollection files)
        {

            long size = files.Sum(f => f.Key.Length);
            //限制文件大小
            if (size > 1024 * 1024 * 5 * 20)
            {
                return BadRequest("文件超过限制大小100MB");
            }
            List<string> filePathREsultList = new List<string>();
            foreach (var file in files.Files)
            {
                //-0-0-0-0-0-0-0-0-------------暂存
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string filePath = ServiceConfigs.FileUpDirectory;
                FilesPrint fp = new FilesPrint();
                string suffix = fileName.Split('.')[1];
                if ((!pictureFormatArray.Contains(suffix)))
                {
                    return StatusCode(500, "文件格式错误");
                }
                fileName = Guid.NewGuid() + "." + suffix;
                string fileFullName = filePath + fileName;
                using (FileStream fs = System.IO.File.Create(fileFullName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                filePathREsultList.Add($"/src/Pictures/{fileName}");
            }
            string message = $"{files.Count} file(s)/{size} bytes uploaded  successfully!";
            return Ok(message);
        }

        /// <summary>
        /// 单个文件上传
        /// </summary>
        /// <param name="type"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json", "multipart/form-data")]//此处为新增
        [HttpPost("only")]
        public IActionResult Post(string type, IFormFile file)
        {

            long size = file.Length;
            //限制文件大小
            if (size > 1024 * 1024 * 5 * 20)
            {
                return BadRequest("文件超过限制大小100MB");
            }
            List<string> filePathREsultList = new List<string>();
      
                //-0-0-0-0-0-0-0-0-------------暂存
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string filePath = ServiceConfigs.FileUpDirectory;
                FilesPrint fp = new FilesPrint();
                string suffix = fileName.Split('.')[1];
                if ((!pictureFormatArray.Contains(suffix)))
                {
                    return StatusCode(500, "文件格式错误");
                }
            filePath+=@"\" + System.DateTime.Now.Year.ToString() + @"\" + System.DateTime.Now.Month.ToString() + @"\" + System.DateTime.Now.Day.ToString();//文件夹
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
                return StatusCode(404, $"存储错误+filefullname={fileFullName}");
                throw;
            }     
        
                filePathREsultList.Add($"/src/Pictures/{fileName}");
            
            string message = $" file(s)/{size} bytes uploaded  successfully! fileurl={fileFullName}";
            return Ok(message);
        }
    }

}
