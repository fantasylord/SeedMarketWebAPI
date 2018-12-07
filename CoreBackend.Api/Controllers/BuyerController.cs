using AutoMapper;
using CoreBackend.Api.Dtos;
using CoreBackend.Api.Entities;
using CoreBackend.Api.Repositories.IRepositories;
using CoreBackend.Api.Services.Iservices;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CoreBackend.Api.Controllers
{
    /// <summary>
    /// 买家
    /// </summary>
    [Route("api/[controller]")]
    public class BuyerController : Controller
    {
        private readonly ILogger<BuyerController> _logger;
        private readonly IMailService _mailService;
        private readonly ISeedRepository _productRepository;
        private readonly IMapper _Mapper;
        public BuyerController(ILogger<BuyerController> logger,
                                    IMailService mailService,
                                    ISeedRepository productRepository)
        {

            _logger = logger;
            _mailService = mailService;
            _productRepository = productRepository;
        }

        /// <summary>
        /// 注册信息
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///     {
        ///      "Name": "张三",
        ///      "Account": "Testone",
        ///      "PW": "123456",
        ///      "Tell": "18888888888",
        ///      "Mail": "123@qq.com",
        ///      "BusinessKey": "蔬菜,水果",
        ///      "LastTime": "2018-05-26T16:16:12.761Z"
        ///     }                 
        /// </remarks>
        /// <param name="buyerdto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterBuyer([FromBody] BuyerCreation buyerCreation)
        {
            if (buyerCreation == null)
            {
                return BadRequest();
            }

            //    buyerdto.LastTime = System.DateTime.Now;
            try
            {
                // var model = _Mapper.Map<Buyer>(buyerCreation);
                var model = new Buyer();
                model.Account = buyerCreation.Account;
                model.BusinessKey = buyerCreation.BusinessKey;
                model.LastTime = buyerCreation.LastTime;
                model.Mail = buyerCreation.Mail;
                model.Name = buyerCreation.Name;
                model.Integral = 0;
                model.PW = buyerCreation.PW;

                model.Tell = buyerCreation.Tell;

                _productRepository.AddBuyer(model);
                if (!_productRepository.Save())
                {
                    return StatusCode(500, "注册失败");
                }
                var dto = Mapper.Map<BuyerCreation>(model);
                return CreatedAtRoute("GetBuyer", new { id = dto.Account }, dto);
            }
            catch (Exception e)
            {
                string s = e.ToString();
                throw;
            }
            return BadRequest();

        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">帐号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        [Route("LoginBuyer")]
        public IActionResult PostLoginBuyer(string account, string password)
        {

            if (password.Equals(_productRepository.GetBuyer(account).PW))
            {
                BuyerDto.BuyerLogins.Add(account, true);
                Response.Cookies.Append("EFSD", account);
                return Ok(Response);
            }
            else
            {
                return StatusCode(500, "用户不存在或密码错误");
            }


        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpGet("{account}")]
        public IActionResult GetBuyer(string account)
        {
            return Ok(_productRepository.GetBuyer(account));
        }

        /// <summary>
        /// 删除用户实体
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpDelete("{account}")]
        public IActionResult DeletBuyer(string account)
        {
            var model = _productRepository.GetBuyer(account);
            if (model != null)
            {

                _productRepository.DeletBuyer(model);
                return Ok();
            }
            else
            { return BadRequest(); }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <param name="buyerdto"></param>
        /// <returns></returns>
        [HttpPatch("{account}")]
        public IActionResult PatchBuyer(string account, [FromBody] JsonPatchDocument<BuyerDto> buyerdto)
        {
            if (buyerdto == null)
            {
                return BadRequest();
            }
            var model = _productRepository.GetBuyer(account);
            if (model == null)
            {
                return NotFound();
            }
            var toPatch = Mapper.Map<BuyerDto>(model);


            buyerdto.ApplyTo(toPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //使用TryValidateModel(xxx)对model进行手动验证, 结果也会反应在ModelState里面.
            TryValidateModel(toPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mapper.Map(toPatch, model);
            if (!_productRepository.Save())
                return StatusCode(500, "更新出错！");
            return NoContent();
        }
        /// <summary>
        /// 修改用户积分
        /// 输入变化量值 例如 传入-5，5，0
        /// </summary>
        /// <param name="account"></param>
        /// <param name="integral"></param>
        /// <returns></returns>
        [HttpPost("{account}/{integral}")]
        public IActionResult AddIntegrals(string  account,int integral)
        {
            if (account == "" || integral == 0)
                return BadRequest();
            var model = _productRepository.GetBuyer(account);

            model.Integral += integral;
            if (!_productRepository.Save())
                return StatusCode(500, "错误改变");
            return Ok(model.Integral);

        }
        
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="buyerDto"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult PutBuyyer([FromBody] BuyerDto buyerDto)
        {
            if (buyerDto == null)
            {
                return BadRequest();
            }
     
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var model = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            var model = _productRepository.GetBuyer(buyerDto.Account);
            // _productRepository.g
            if (model == null)
            {
                return NotFound();
            }
            //这个方法会把第一个对象相应的值赋给第二个对象上
            Mapper.Map(buyerDto, model);
            if (!_productRepository.Save())
            {
                return StatusCode(500, "保存产品出错");
            }
            return NoContent();
        }
    }
}
