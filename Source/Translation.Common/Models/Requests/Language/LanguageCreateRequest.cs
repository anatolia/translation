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

        public LanguageCreateRequest(long currentUserId, string name, string originalName, string isoCode2,
                                     string isoCode3, string icon, string description) : base(currentUserId)
        {
            if (name.IsEmpty())
            {
                ThrowArgumentException(nameof(name), name);
            }

            if (originalName.IsEmpty())
            {
                ThrowArgumentException(nameof(originalName), originalName);
            }

            if (isoCode2.IsEmpty()
                || isoCode2.Length != 2)
            {
                ThrowArgumentException(nameof(isoCode2), isoCode2);
            }

            if (isoCode3.IsEmpty()
                || isoCode3.Length != 3)
            {
                ThrowArgumentException(nameof(isoCode3), isoCode3);
            }

            if (icon.IsEmpty())
            {
                ThrowArgumentException(nameof(icon), icon);
            }

            Name = name;
            OriginalName = originalName;
            IsoCode2 = isoCode2;
            IsoCode3 = isoCode3;
            Icon = icon;
            Description = description;
        }
    }
}