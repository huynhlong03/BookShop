using BookShop_WebApplication.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace BookShop_WebApplication.Filters
{
    public class AdminAuthorization : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = SessionConfig.GetUser();
            var admin = SessionConfig.GetAdmin();

            if (user == null && admin == null)
            {
                // User is not logged in, redirect to login page
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                { "controller", "Home" },
                { "action", "Login" },
                { "area", null } // For non-area controller
                    });
            }
            else if (user != null)
            {
                // User is logged in as user, redirect to home page
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                { "controller", "Home" },
                { "action", "Index" },
                { "area", null } // For non-area controller
                    });
            }
            else if (admin != null)
            {
                // User is logged in as admin, check if action is in admin area
                bool isInAdminArea = string.Equals(filterContext.RouteData.DataTokens["area"] as string, "Admin", StringComparison.OrdinalIgnoreCase);
                if (isInAdminArea)
                {
                    // Action is in admin area, allow access
                }
                else
                {
                    // Action is not in admin area, redirect to admin home page
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                    { "controller", "HomeAdmin" },
                    { "action", "Index" },
                    { "area", "Admin" }
                        });
                }
            }  
            }
    }
}