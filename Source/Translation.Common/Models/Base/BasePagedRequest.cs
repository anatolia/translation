﻿using Translation.Common.Models.Shared;

namespace Translation.Common.Models.Base
{
    public abstract class BasePagedRequest : BaseRequest
    {
        public string SearchTerm { get; set; }

        /// <summary>
        /// if skip is greater than 0
        /// service does not checks for LastUid,
        /// To use last uid ensure Skip is 0
        /// </summary>
        public PagingInfo PagingInfo { get; set; }

        protected BasePagedRequest()
        {
            PagingInfo = new PagingInfo();
        }
    }
}