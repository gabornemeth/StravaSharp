using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    [TestFixture("2011-08-10-17-51-31.fit")]
    public class UploadTest
    {
        private string _fileName;

        public UploadTest(string fileName)
        {
            _fileName = fileName;
        }

        [Test]
        public async Task UploadAndDelete()
        {
            var client = TestHelper.CreateStravaClient();
            using (var stream = TestHelper.GetResourceStream(_fileName))
            {
                // upload the activity
                var result = await client.Activities.Upload(ActivityType.Ride, DataType.Fit, stream, _fileName);
                Assert.IsNotNull(result);
                Assert.IsNullOrEmpty(result.Error);
                // wait till upload has completed
                while (result.ActivityId == 0)
                {
                    result = await client.Activities.GetUploadStatus(result.Id);
                    await Task.Delay(8000);
                }
                // delete the ready activity
                await client.Activities.Delete(result.ActivityId);
            }
        }
    }
}
