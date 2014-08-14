﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using G16_2013.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace G16MG
{
    public partial class Startup
    {
        // 如需設定驗證的詳細資訊，請造訪 http://go.microsoft.com/fwlink/?LinkId=301883
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and role manager to use a single instance per request
            app.CreatePerOwinContext(G16MemberContext.Create);
            app.CreatePerOwinContext<G16ApplicationUserManager>(G16ApplicationUserManager.Create);
            app.CreatePerOwinContext<G16ApplicationRoleManager>(G16ApplicationRoleManager.Create);

            // 啟用應用程式以使用 Cookie 來儲存已登入使用者的資訊
            // 也儲存透過第三方登入提供者登入的使用者資訊。
            // 若您的應用程式允許使用者登入，則此為必要項目
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // 取消註解下列行以啟用透過第三方登入提供者的登入
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication();
        }
    }
}