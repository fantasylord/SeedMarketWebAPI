using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackend.Api.Controllers.Autoorization
{
    /// <summary>
    /// 身份验证集中控制
    /// </summary>
    [Route("api/[controller]")]
    public class AuthController:Controller
    {

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task Logout()
        {

            try
            {
                await HttpContext.SignOutAsync("Cookies");
                await HttpContext.SignOutAsync("oidc");
            }
            catch (Exception)
            {

                throw;
            }
            Ok("cancelled");
        }
    }
}
