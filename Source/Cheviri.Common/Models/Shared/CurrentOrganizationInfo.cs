using System.Collections.Generic;

namespace Cheviri.Common.Models.Shared
{
    public class CurrentOrganizationInfo
    {
        public string Uid { get; set; }
        public string Name { get; set; }

        public List<OnlineUserInfo> OnlineUsers { get; set; }

        public CurrentOrganizationInfo()
        {
            OnlineUsers = new List<OnlineUserInfo>();
        }
    }
}