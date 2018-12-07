using CoreBackend.Api.Services.Iservices;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackend.Api.Services
{
    public class CloudMailService : IMailService
    {
        private readonly string _mailTo = "admin@qq.com";
        private readonly string _mailFrom = "noreply@alibaba.com";
        private readonly ILogger<CloudMailService> _logger;
        public CloudMailService(ILogger<CloudMailService> logger)
        {
            _logger = logger;
        }
        public void Send(string subject, string msg)
        {
            _logger.LogInformation($"从{_mailFrom}给{_mailTo}通过{nameof(LocalMailService)}发送了邮件");
        }
    }
}
