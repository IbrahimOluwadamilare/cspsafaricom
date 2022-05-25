using Hangfire.Dashboard;

namespace cspv3.Helpers
{

    public class HangfireAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            //  return httpContext.User.Identity.IsAuthenticated;


            return httpContext.User.IsInRole("Admin");
        }
    }
}
