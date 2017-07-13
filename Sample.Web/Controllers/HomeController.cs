using RestSharp.Portable.OAuth2;
using Sample.Web.ViewModels;
using StravaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var authenticator = CreateAuthenticator();
            var viewModel = new HomeViewModel(authenticator.IsAuthenticated);
            if (authenticator.IsAuthenticated)
            {
                var client = new StravaSharp.Client(authenticator);
                var activities = await client.Activities.GetAthleteActivities();
                foreach (var activity in activities)
                {
                    viewModel.Activities.Add(new ActivityViewModel(activity));
                }
            }
            return View(viewModel);
        }

        Authenticator CreateAuthenticator()
        {
            var redirectUrl = $"{Request.Url.Scheme}://{Request.Url.Host}:{Request.Url.Port}/Home/Callback";
            var config = new RestSharp.Portable.OAuth2.Configuration.RuntimeClientConfiguration
            {
                IsEnabled = false,
                ClientId = Config.ClientId,
                ClientSecret = Config.ClientSecret,
                RedirectUri = redirectUrl,
                Scope = "write,view_private",
            };
            var client = new StravaClient(new Authentication.RequestFactory(), config);

            return new Authenticator(client);
        }

        public async Task<ActionResult> List()
        {
            var authenticator = CreateAuthenticator();
            var loginUri = await authenticator.GetLoginLinkUri();

            return Redirect(loginUri.AbsoluteUri);
        }

        public async Task<ActionResult> Callback()
        {
            var authenticator = CreateAuthenticator();
            await authenticator.OnPageLoaded(Request.Url);
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}