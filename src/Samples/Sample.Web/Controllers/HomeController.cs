using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample.ViewModels;
using Sample.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Sample.Web.Authentication;

namespace Sample.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticatorHolder _authenticatorHolder;

        public HomeController(IHttpContextAccessor httpContextAccessor, IAuthenticatorHolder authenticatorHolder)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _authenticatorHolder = authenticatorHolder ?? throw new ArgumentNullException(nameof(authenticatorHolder));
        }

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

        WebAuthenticator CreateAuthenticator()
        {
            if (_authenticatorHolder.Authenticator != null)
            {
                return _authenticatorHolder.Authenticator;
            }
            var redirectUrl = $"{Request.Scheme}://{Request.Host}/Home/Callback";
            var client = Config.CreateOAuth2Cient(config => config.RedirectUri = redirectUrl);

            var authenticator = new WebAuthenticator(client, _httpContextAccessor);
            _authenticatorHolder.Authenticator = authenticator;
            return authenticator;
        }

        public ActionResult List()
        {
            var authenticator = CreateAuthenticator();
            if (authenticator.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            var loginUri = authenticator.GetLoginLinkUri();
            return Redirect(loginUri.AbsoluteUri);
        }

        public async Task<ActionResult> Callback()
        {
            var authenticator = CreateAuthenticator();
            await authenticator.OnPageLoaded(new Uri(Request.GetDisplayUrl()));
            return RedirectToAction("Index");
        }
    }
}
