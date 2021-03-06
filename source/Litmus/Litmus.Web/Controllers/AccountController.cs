﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IdentityModel.Services;
using System.IdentityModel.Services.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Litmus.Web.Models;

namespace Litmus.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult SignOut()
        {
            var config = FederatedAuthentication.FederationConfiguration.WsFederationConfiguration;

            // Redirect to SignOutCallback after signing out.
            string callbackUrl = Url.Action("", "", routeValues: null, protocol: Request.Url.Scheme);
            var signoutMessage = new SignOutRequestMessage(new Uri(config.Issuer), callbackUrl);
            signoutMessage.SetParameter("wtrealm", IdentityConfig.Realm ?? config.Realm);
            FederatedAuthentication.SessionAuthenticationModule.SignOut();

            return new RedirectResult(signoutMessage.WriteQueryString());
        }
    }
}