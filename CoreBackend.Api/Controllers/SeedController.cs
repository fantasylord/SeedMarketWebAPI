using AutoMapper;
using CoreBackend.Api.Dtos;
using CoreBackend.Api.Entities;
using CoreBackend.Api.Repositories.IRepositories;
using CoreBackend.Api.Services.Iservices;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using static CoreBackend.Api.Utils.FileStreamPandO;

namespace CoreBackend.Api.Controllers
{
    /// <summary>
    /// 商品信息
    /// </summary>
    [Route("api/[controller]")]
    public class SeedController : Controller
    {
        private readonly ILogger<SeedController> _logger;
        private readonly IMailService _mailService;
        private readonly ISeedRepository _productRepository;
        private readonly IMapper _Mapper;


        public SeedController(ILogger<SeedController> logger,
                                IMailService mailService,
                                ISeedRepository seedRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _productRepository = seedRepository;
        }
        /// <summary>
        /// 获取唯一请传seedid查询参数
        /// </summary>
        /// <param name="brand">模糊查询 value=""</param>

        /// <param name="sellerid">模糊查询value=-1</param>
        /// <returns>List<seeddto></returns>
        [HttpGet("getSeeds")]
        public IActionResult getSeeds(string brand = "", int sellerid = -1)
        {
            var models = _productRepository.GetSeeds(sellerid, brand).ToList<Seed>();
            if (models == null)
                return StatusCode(500, "获取失败");
            var list = new List<SeedDto>();
            foreach (Seed model in models)
            {
                list.Add(new SeedDto
                {
                    SellerID = model.SellerID,
                    Species = model.Species,
                    Brand = model.Brand,
                    Details = model.Details,
                    SeedClass = model.SeedClass,
                    Exhibitions = model.Exhibitions,
                    Name = model.Name,
                    MakertID = model.MakertID,
                    MakertName = _productRepository.GetSeller(model.SellerID).MarkerName,
                    Price = model.Price
                });
            }


            return Ok(list);

        }
        /// <summary>
        /// 获取带图片信息的种子
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        [HttpGet("haveimg/{seedid}")]
        public IActionResult getSeedImg(int seedid)
        {

            if (seedid < 0)
                return BadRequest();
            var model = _productRepository.GetSeed(seedid);
            if (model == null)
                return StatusCode(500, "获取失败");
            //var list = new List<SeedDto>();
            SeedAndImgDto seedDto = new SeedAndImgDto
            {
                SellerID = model.SellerID,
                Species = model.Species,
                Brand = model.Brand,
                Details = model.Details,
                SeedClass = model.SeedClass,
                Exhibitions = model.Exhibitions,
                MakertID = model.MakertID,
                MakertName = _productRepository.GetSeller(model.SellerID).MarkerName,
                Name = model.Name,
            
                Price = model.Price,


            };
            var filemodel = _productRepository.GetFile(model.SeedID);
            FileUpDownLoadToByteDto filedto = new FileUpDownLoadToByteDto
            {
                FID = filemodel.FID,
                FileClass = filemodel.FileClass,
                FileUrl = filemodel.FileUrl,
                FileName = filemodel.FileName,
                SeedID = filemodel.seed.SeedID
            };

            try
            {
                string Imgurl = _productRepository.GetFile(model.SeedID).FileUrl;
                FilesPrint fp = new FilesPrint();

              filedto.Bytes = fp.ReadFile(fp.readURL(Imgurl));
                seedDto.FileUpDownLoadDto = filedto;
            }
            catch (Exception)
            {
                filedto.Bytes= null;
                throw;
            }


            return Ok(seedDto);

        }
        /// <summary>
        /// 获取种子商品信息
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        [HttpGet("{seedid}")]
        public IActionResult getSeed(int seedid)
        {
            try
            {
                if (seedid < 0)
                    return BadRequest();
                var model = _productRepository.GetSeed(seedid);
                if (model == null)
                    return StatusCode(500, "获取失败");
                //var list = new List<SeedDto>();
                SeedDto seedDto = new SeedDto
                {
                    SellerID = model.SellerID,
                    Species = model.Species,
                    Brand = model.Brand,
                    Details = model.Details,
                    SeedClass = model.SeedClass,
                    Exhibitions = model.Exhibitions,
                    MakertID = model.MakertID,
                    MakertName = _productRepository.GetSeller(model.SellerID).MarkerName,
                    Name = model.Name,
                    Price = model.Price
                };



                return Ok(seedDto);
            }
            catch (Exception e)
            {
                string s = e + "";
                throw;
            }
            return NoContent();

        }

        [HttpPost("haveimg")]
        public IActionResult PostSeedImg([FromBody] SeedAndImgDto seedDto)
        {
            if (seedDto == null)
                return BadRequest();
            var model = new Seed
            {

                SellerID = seedDto.SellerID,
                Species = seedDto.Species,
                Brand = seedDto.Brand,
                Details = seedDto.Details,
                SeedClass = seedDto.SeedClass,
                Exhibitions = seedDto.Exhibitions,
                Name = seedDto.Name,

                Price = seedDto.Price
            };
            if (seedDto.FileUpDownLoadDto != null)
            {
                FileUpDownload fileUpDownload = new FileUpDownload
                {
                    FileClass = seedDto.FileUpDownLoadDto.FileClass,
                    FileName = seedDto.FileUpDownLoadDto.FileName,
                    seed = _productRepository.GetSeed(seedDto.SeedID),
                    

                };

                try
                {
                    string fileurl;
                    FilesPrint fp = new FilesPrint();
                    fileurl = fp.PrintFileCreate(ServiceConfigs.FileUpDirectory, seedDto.FileUpDownLoadDto.Bytes);
                    fileUpDownload.FileUrl = fp.readURL(fileurl);
                    model.fileUpDownloads.Add(fileUpDownload);
                }
                catch (Exception)
                {
                    return StatusCode(500, "传入字节流文件错误");
                    throw;
                }
               
            }
           
            _productRepository.AddSeed(model);
            if (!_productRepository.Save())
            {
                return StatusCode(500, "存储失败");
            }
            return Ok();
        }
        /// <summary>
        /// 提交种子商品信息
        /// </summary>
        /// <param name="seedDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult postSeed([FromBody] SeedDto seedDto)
        {
            if (seedDto == null)
                return BadRequest();
            var model = new Seed
            {

                SellerID = seedDto.SellerID,
                Species = seedDto.Species,
                Brand = seedDto.Brand,
                Details = seedDto.Details,
                SeedClass = seedDto.SeedClass,
                Exhibitions = seedDto.Exhibitions,
                Name = seedDto.Name,

                Price = seedDto.Price
            };
            _productRepository.AddSeed(model);
            if (!_productRepository.Save())
            {
                return StatusCode(500, "存储失败");
            }
            return Ok();
        }
        /// <summary>
        /// 修改种子商品
        /// </summary>
        /// <param name="seedDto"></param>
        /// <returns></returns>
        [HttpPut("seedid")]
        public IActionResult PutSeed(int seedid, [FromBody] SeedDto seedDto)
        {
            var model = _productRepository.GetSeed(seedid);
            if (model == null)
            { return BadRequest("无数据"); }
            var modelput = new Seed
            {
                SeedID = seedDto.SeedID,
                SellerID = seedDto.SellerID,
                Species = seedDto.Species,
                Brand = seedDto.Brand,
                Details = seedDto.Details,
                SeedClass = seedDto.SeedClass,
                Exhibitions = seedDto.Exhibitions,
                Name = seedDto.Name,
                Price = seedDto.Price
            };

            model.Name = modelput.Name;
            model.Price = modelput.Price;
            model.SellerID = modelput.SellerID;
            model.Species = modelput.Species;
            model.Exhibitions = modelput.Exhibitions;
            model.Details = modelput.Details;
            model.Brand = modelput.Brand;
            model.SeedClass = modelput.SeedClass;

            if (!_productRepository.Save())
            {
                return StatusCode(500, "存储失败");
            }
            return NoContent();
        }

        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="jsonPatch"></param>
        /// <returns></returns>
        [HttpPatch("seedid")]
        public IActionResult PatchSeed(int seedid, [FromBody] JsonPatchDocument<SeedDto> jsonPatch)
        {

            if (jsonPatch == null)
            { return BadRequest(); }
            var model = _productRepository.GetSeed(seedid);

            if (model == null)
            {
                return NotFound();
            }
            SeedDto patch = new SeedDto
            {



                SellerID = model.SellerID,
                Species = model.Species,
                Brand = model.Brand,
                Details = model.Details,
                SeedClass = model.SeedClass,
                Exhibitions = model.Exhibitions,
                Name = model.Name,
                Price = model.Price
            };

            jsonPatch.ApplyTo(patch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TryValidateModel(patch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //SeedID=
            //SellerID,
            //species,
            //Brand,
            //Details,
            //Class,
            //Exhibitions,
            //Name,
            //Price


            model.Species = patch.Species;
            model.Brand = patch.Brand;
            model.Details = patch.Details;
            model.SeedClass = patch.SeedClass;
            model.Name = patch.Name;
            model.Price = patch.Price;
            if (!_productRepository.Save())
                return StatusCode(500, "更新出错");
            return NoContent();
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        [HttpDelete("seedid")]
        public IActionResult Delete(int seedid)
        {
            var getresult = _productRepository.GetSeed(seedid);
            if (getresult == null)
                return BadRequest();
            _productRepository.DeleteSeed(getresult);
            if (!_productRepository.Save())
                return StatusCode(500, "数据删除失败");
            return NoContent();
        }

    }
}
