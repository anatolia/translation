using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Language
{
    public sealed class LanguageCreateRequest : BaseAuthenticatedRequest
    {
        public string Name { get; }
        public string OriginalName { get; }
        public string IsoCode2 { get; }
        public string IsoCode3 { get; }
        public string Icon { get; }
        public string Description { get; }

        public LanguageCreateRequest(
            long currentUserId, 
            string name, 
            string originalName, 
            string isoCode2,
            string isoCode3, 
            string icon, 
            string description) : base(currentUserId)
        {
            Name = name.ThrowIfNullOrEmpty(nameof(name));
            OriginalName = originalName.ThrowIfNullOrEmpty(nameof(originalName));
            IsoCode2 = isoCode2.ThrowIfNullOrEmpty(nameof(name), isoCode2.Length != 2);
            IsoCode3 = isoCode3.ThrowIfNullOrEmpty(nameof(name), isoCode3.Length != 3);
            Icon = icon.ThrowIfNullOrEmpty(nameof(icon));
            Description = description;
        }
    }
}