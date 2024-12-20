//
// SegmentTest.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using System;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    public class Test
    {
        protected async Task GoOnIfPremium(Client client, Func<Task> action)
        {
            var currentUser = await client.Athletes.GetCurrent();
            if (currentUser.Premium)
            {
                await action();
            }
        }
    }
}
