﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace Barnamenevisan_MVC.Classes
{
    public static class PartialToStringClass
    {
        //public static string RenderPartialToString(ControllerContext ControllerContext,string viewName, object model)
        //{
        //    if (string.IsNullOrEmpty(viewName))
        //        viewName = ControllerContext.RouteData.GetRequiredString("action");
        //    ViewDataDictionary ViewData = new ViewDataDictionary();
        //    TempDataDictionary TempData = new TempDataDictionary();
        //    ViewData.Model = model;

        //    using (StringWriter sw = new StringWriter())
        //    {
        //        ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
        //        ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
        //        viewResult.View.Render(viewContext, sw);

        //        return sw.GetStringBuilder().ToString();
        //    }

        //}

        public static string RenderPartialView(string controllerName, string partialView, object model)
        {
            var context = new HttpContextWrapper(System.Web.HttpContext.Current) as HttpContextBase;

            var routes = new System.Web.Routing.RouteData();
            routes.Values.Add("controller", controllerName);

            var requestContext = new RequestContext(context, routes);

            string requiredString = requestContext.RouteData.GetRequiredString("controller");
            var controllerFactory = ControllerBuilder.Current.GetControllerFactory();
            var controller = controllerFactory.CreateController(requestContext, requiredString) as ControllerBase;

            controller.ControllerContext = new ControllerContext(context, routes, controller);

            var ViewData = new ViewDataDictionary();

            var TempData = new TempDataDictionary();

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, partialView);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, ViewData, TempData, sw);

                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

    }
}