using AutoMapper;
using CoreBackend.Api.Dtos;
using CoreBackend.Api.Entities;
using CoreBackend.Api.Repositories.IRepositories;
using CoreBackend.Api.Services.Iservices;
using CoreBackend.Api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static CoreBackend.Api.Utils.FileStreamPandO;

namespace CoreBackend.Api.Controllers
{
    /// <summary>
    /// 文件上传如图片
    /// </summary>
    [Route("api/[controller]")]
    public class FileUpDownloadController: Controller
    {
        private readonly ILogger<FileUpDownloadController> _logger;
        private readonly IMailService _mailService;
        private readonly ISeedRepository _productRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _Mapper;
        public FileUpDownloadController(ILogger<FileUpDownloadController> logger,
                                    IMailService mailService,IFileService fileService,
                                    ISeedRepository productRepository)
        {
            _fileService = fileService;
            _logger = logger;
            _mailService = mailService;
            _productRepository = productRepository;
        }

        /// <summary>
        /// 通过文件方式上传
        /// </summary>
        /// <param name="file"></param>
        /// <param name="dtos"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json", "multipart/form-data")]//此处为新增
        [HttpPost]
        public IActionResult AddSeedAndImg(IFormFile file, FileUpDownLoadGetDto dtos)
        {
            if (file == null || file.ContentDisposition.Length<=0||file.FileName==null)
                return BadRequest("空异常");
            FileUpDownLoadAndFileGetDto dto = new FileUpDownLoadAndFileGetDto
            {
                file=file,
                FileClass=dtos.FileClass,
                FileUrl=dtos.FileUrl,
                SeedID=dtos.SeedID
            };

           string fileUrl= _fileService.PrintFileCreate(dto.file);
            FileStreamPandO.FilesPrint fp = new FilesPrint();
            fileUrl= fp.SaveSRC(fileUrl);
            if (fileUrl == null)
                return StatusCode(500, "存储错误");
            if (_productRepository.GetSeed(dto.SeedID) == null)
                return StatusCode(500, "不存在该编号产品");
            var model = new FileUpDownload()
            {
                FileClass = dto.FileClass,
                FileName = file.FileName,
                FileUrl = fileUrl,

                seed = _productRepository.GetSeed(dto.SeedID)
            };
            _productRepository.AddFile(model);
            if (!_productRepository.Save())
            {
                return StatusCode(500, "上传失败，转储数据库异常");
            }
            return NoContent();
        }
        /// <summary>
        /// 提交带有图片的种子实体信息-字节流
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("tobyte")]
        public IActionResult AddSeedAndImg([FromBody] FileUpDownLoadToByteDto dto)
        {
            if (dto.Bytes == null || dto.FileName == null)
                return BadRequest("空异常");
            FilesPrint fhelp = new FilesPrint();
            string fileUrl = fhelp.PrintFileCreate(ServiceConfigs.FileUpDirectory, dto.Bytes);
            if (fileUrl == null)
                return StatusCode(500,"存储错误");
            if (_productRepository.GetSeed(dto.SeedID) == null)
                return StatusCode(500,"不存在该编号产品");
            var model = new FileUpDownload()
            {
                FileClass = dto.FileClass,
                FileName = dto.FileName,
                FileUrl = fileUrl,
             
                seed = _productRepository.GetSeed(dto.SeedID)
            };
            _productRepository.AddFile(model);
            if (!_productRepository.Save())
            {
                return StatusCode(500, "上传失败，转储数据库异常");
            }
            return NoContent();
        }

        /// <summary>
        /// 获取种子的图片字节流
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        [HttpGet("tobyte/{seedid}")]
        public IActionResult GetSeedndImg(int seedid)
        {

            if (_productRepository.GetSeed(seedid) == null)
                return StatusCode(500,"不存在");
            
            var model = _productRepository.GetFile(seedid);
            FilesPrint fhelp = new FilesPrint();
            byte[] bt = fhelp.ReadFile(fhelp.readURL(model.FileUrl));
            if (bt == null)
                return StatusCode(500, "读取异常");
            return Ok(new FileUpDownLoadToByteDto
            {
                FID = model.FID,
                FileClass = model.FileClass,
                FileName = model.FileName,
              
                SeedID = model.seed.SeedID,
                Bytes = bt

            });
          
        }
        /// <summary>
        /// 修改应注意只需要修改文件内容即bytes[]
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("tobyte/{seedid}")]
        public IActionResult PutSeedAndImg(int seedid,[FromBody] FileUpDownLoadToByteDto dto)
        {
            if (dto.Bytes == null || dto.FileName == null)
                return BadRequest("空异常");
            FilesPrint fhelp = new FilesPrint();
            var url = _productRepository.GetFile(seedid).FileUrl;
            if (url == null)
                StatusCode(500, "服务器文件丢失");
            ;
            int fileUrl = fhelp.PrintFileUpdate(fhelp.readURL(url), dto.Bytes);
            if (fileUrl == 0)
                return StatusCode(500, "存入失败");
            if (fileUrl == null)
                return StatusCode(500, "存储错误");
            if (_productRepository.GetSeed(dto.SeedID) == null)
                return StatusCode(500, "不存在该编号产品");
            var model = _productRepository.GetFile(dto.SeedID);

            model.FileClass = dto.FileClass;
            model.FileName = dto.FileName;
            model.FileUrl = dto.FileUrl;
         
            model.seed = _productRepository.GetSeed(dto.SeedID);
          
            

            if (!_productRepository.Save())
            {
                return StatusCode(500, "上传失败，转储数据库异常");
            }
            return NoContent();
        }

    }
}
