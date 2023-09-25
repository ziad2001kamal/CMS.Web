using CMS.Core.Dtos;
using CMS.Core.Enums;
using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        public async Task<bool> SendByFCM(string token, NotificationDto dto)
        {
            try
            {


                var notifiication = new Message()
                {
                    Data = new Dictionary<string, string>
            {
                {"Title",dto.Title },
                {"Body",dto.Body },
                {"Action",dto.Action.ToString() },
                {"ActionId",dto.ActionId },
            },
                    Token = token
                };
                await FirebaseMessaging.DefaultInstance.SendAsync(notifiication);
                return true;
            }catch (Exception e) {
                return false;
            }
        }
    }
}
