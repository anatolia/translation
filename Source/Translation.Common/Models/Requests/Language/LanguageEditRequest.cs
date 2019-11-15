using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Language
{
    public sealed class LanguageEditRequest : BaseAuthenticatedRequest
    {
        public Guid LanguageUid { get; }
        public string Name { get; }
        public string OriginalName { get; }
        public string IsoCode2 { get; }
        public string IsoCode3 { get; }
        public string Icon { get; }
        public string Description { get; }

        public LanguageEditRequest(long currentUserId, Guid languageUid, string name, string originalName,
                                   string isoCode2, string isoCode3, string icon,
                                   string description) : base(currentUserId)
        {
            if (languageUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(languageUid), languageUid);
            }

            name = name.TrimOrDefault();
            if (name.IsEmpty())
            {
                ThrowArgumentException(nameof(name), name);
            }

            originalName = originalName.TrimOrDefault();
            if (originalName.IsEmpty())
            {
                ThrowArgumentException(nameof(originalName), originalName);
            }

            isoCode2 = isoCode2.TrimOrDefault();
            if (isoCode2.IsEmpty()
                || isoCode2.Length != 2)
            {
                ThrowArgumentException(nameof(isoCode2), isoCode2);
            }

            isoCode3 = isoCode3.TrimOrDefault();
            if (isoCode3.IsEmpty()
                || isoCode3.Length != 3)
            {
                ThrowArgumentException(nameof(isoCode3), isoCode3);
            }

            icon = icon.TrimOrDefault();
            if (icon.IsEmpty())
            {
                ThrowArgumentException(nameof(icon), icon);
            }

            LanguageUid = languageUid;
            OriginalName = originalName;
            Name = name;
            IsoCode2 = isoCode2;
            IsoCode3 = isoCode3;
            Icon = icon;
            Description = description;
        }
    }
}