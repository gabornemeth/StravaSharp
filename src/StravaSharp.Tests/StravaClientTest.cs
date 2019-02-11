using NSubstitute;
using NUnit.Framework;
using RestSharp.Portable.OAuth2.Configuration;
using RestSharp.Portable.OAuth2.Infrastructure;
using RestSharp.Portable.OAuth2.Models;

namespace StravaSharp.Tests
{
    /// <summary>
    /// Tests of the <see cref="StravaClient"/> class.
    /// </summary>
    [TestFixture]
    public class StravaClientTest
    {
        [Test]
        public void ParseUserInfo()
        {
            var client = new StravaClientForTest(Substitute.For<IRequestFactory>(), Substitute.For<IClientConfiguration>());
            var userInfo = client.ParseUserInfo(Resource.GetText(Resource.UserInfoJson));
            Assert.NotNull(userInfo);
            Assert.False(string.IsNullOrEmpty(userInfo.FirstName));
            Assert.False(string.IsNullOrEmpty(userInfo.LastName));
            Assert.False(string.IsNullOrEmpty(userInfo.PhotoUri));
        }

        class StravaClientForTest : StravaClient
        {
            public StravaClientForTest(IRequestFactory factory, IClientConfiguration configuration)
                    : base(factory, configuration)
            {
            }

            public new UserInfo ParseUserInfo(string content) // make visible for other classes
            {
                return base.ParseUserInfo(content);
            }
        }
    }
}
