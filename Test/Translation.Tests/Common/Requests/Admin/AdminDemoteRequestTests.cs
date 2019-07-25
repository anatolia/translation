using System;
using System.Collections;
using NUnit.Framework;

using static Translation.Tests.TestHelpers.FakeRequestTestHelper;

namespace Translation.Tests.Common.Requests.Admin
{
    [TestFixture]
    public class AdminDemoteRequestTests
    {
        [Test]
        public void AdminDemoteRequest_Constructor()
        {
            var request = GetAdminDemoteRequest();

        }

    }
}