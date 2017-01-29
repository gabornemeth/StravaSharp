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
            // cannot return null
            return new UserInfo();
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
