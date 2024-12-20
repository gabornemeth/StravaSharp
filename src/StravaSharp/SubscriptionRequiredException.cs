//
// Extensions.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2024, Gabor Nemeth
//

using System;

namespace StravaSharp
{
    internal class SubscriptionRequiredException : Exception
    {
        private const string ErrorMessage = "Strava Premium subscription is required.";
        
        public SubscriptionRequiredException() : base(ErrorMessage)
        {
        }

        public SubscriptionRequiredException(Exception innerException) : base(ErrorMessage, innerException)
        {
        }
    }
}