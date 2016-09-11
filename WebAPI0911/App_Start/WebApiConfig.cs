using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace WebAPI0911
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務
            // 將 Web API 設定成僅使用 bearer 權杖驗證。
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",

                //不需要加ACTIONNAME  因為會使用HTTP動詞來自動處理符合Restful精神
                //下面的路由是MVC用
                //routeTemplate: "api/{controller}/{action}/{id}",
                //下面是本機端的路由
                //http://localhost:13838/api/Products/GetProduct/1 

                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
