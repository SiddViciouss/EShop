using MailChimp.Net;
using MailChimp.Net.Core;
using MailChimp.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Web.Services
{
    public class MailChimpEmailService: IEmailService
    {
        private const string ApiKey = "";
        private const int TemplateId = 9999;
        private const string ListId = "";

        private MailChimpManager _mailChimpManager = new MailChimpManager(ApiKey);

        private Setting _campaignSettings = new Setting
        {
            ReplyTo = "asd",
            FromName = "42 studio admin",
            Title = "Регистрация нового пользователя",
            SubjectLine = "Регистрация на сайте 42studio.org",
        };

        public void CreateAndSendCampaign(string html)
        {
            var campaign = _mailChimpManager.Campaigns.AddAsync(new Campaign
            {
                Settings = _campaignSettings,
                Recipients = new Recipient { ListId = ListId },
                Type = CampaignType.Regular
            }).Result;
            var timeStr = DateTime.Now.ToString();
            var content = _mailChimpManager.Content.AddOrUpdateAsync(
             campaign.Id,
             new ContentRequest()
             {
                 Template = new ContentTemplate
                 {
                     Id = TemplateId,
                     Sections = new Dictionary<string, object>()
                    {
                        { "body_content", html },
                        { "preheader_leftcol_content", $"<p>{timeStr}</p>" }
                    }
                 }
             }).Result;
            _mailChimpManager.Campaigns.SendAsync(campaign.Id).Wait();
        }

        public List<Template> GetAllTemplates() => _mailChimpManager.Templates.GetAllAsync().Result.ToList();

        public List<List> GetAllMailingLists() => _mailChimpManager.Lists.GetAllAsync().Result.ToList();
        public Content GetTemplateDefaultContent(string templateId) => (Content)_mailChimpManager.Templates.GetDefaultContentAsync(templateId).Result;

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return null;
        }
    }
}
