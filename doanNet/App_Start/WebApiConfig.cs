using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace doanNet
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            // Configure JSON formatter
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            jsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));

            // Configure JSON serializer settings
            var serializerSettings = jsonFormatter.SerializerSettings;
            serializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects; // Preserve object references
            serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; // Ignore circular references
            // Remove XML formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }

    }
}
