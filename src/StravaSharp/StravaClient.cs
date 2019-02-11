//
// StravaClient.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using System;
using RestSharp.Portable;
using RestSharp.Portable.OAuth2;
using RestSharp.Portable.OAuth2.Infrastructure;
using RestSharp.Portable.OAuth2.Models;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace StravaSharp
{
    /// <summary>
    /// RestSharp.Portable Client configuration for Strava
    /// </summary>
    public class StravaClient : OAuth2Client
    {
		/// <summary>
		/// Base URL of the API
		/// </summary>
        public const string ApiBaseUrl = "https://www.strava.com";

        public StravaClient(IRequestFactory factory, RestSharp.Portable.OAuth2.Configuration.IClientConfiguration configuration)
            : base(factory, configuration)
        {
        }

        protected override Endpoint AccessCodeServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = ApiBaseUrl,
                    Resource = "/oauth/authorize"
                };
            }
        }

        protected override Endpoint AccessTokenServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = ApiBaseUrl,
                    Resource = "/oauth/token"
                };
            }
        }

		/// <summary>
		/// Name of the service
		/// </summary>
        public override string Name
        {
            get { return "strava"; }
        }

        protected override UserInfo ParseUserInfo(IRestResponse response)
        {
            return ParseUserInfo(response.Content);
        }

        /// <summary>
        /// Parsing user info
        /// This method can be overriden in test.
        /// </summary>
        /// <param name="content">user info in JSON</param>
        /// <returns></returns>
        protected virtual UserInfo ParseUserInfo(string content)
        {
            // we receive the inner of "athlete" node as content
            var obj = JObject.Parse(content);
            var userInfo = new UserInfo
            {
                Id = obj["id"].Value<string>(),
                FirstName = obj["firstname"].Value<string>(),
                LastName = obj["lastname"].Value<string>(),
            };
            userInfo.AvatarUri.Normal = obj["profile_medium"].Value<string>();
            userInfo.AvatarUri.Large = obj["profile"].Value<string>();
            return userInfo;
        }

        protected override void BeforeGetUserInfo(BeforeAfterRequestArgs args)
        {
            args.Request.Parameters.Add(new Parameter { Name = "access_token", Value = AccessToken, Type = ParameterType.GetOrPost });
            base.BeforeGetUserInfo(args);
        }

        protected override Endpoint UserInfoServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = ApiBaseUrl,
                    Resource = "/api/v3/athlete"
                };
            }
        }
    }
}
