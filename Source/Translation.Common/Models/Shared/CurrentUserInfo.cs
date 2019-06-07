namespace Translation.Common.Models.Shared
{
    public class CurrentUserInfo
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public CurrentOrganizationInfo CurrentOrganizationInfo { get; set; }
    }
}