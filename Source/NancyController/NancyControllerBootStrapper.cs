﻿using Nancy;

namespace NancyController
{
    public class NancyControllerBootStrapper : DefaultNancyBootstrapper
    {
        protected override IRootPathProvider RootPathProvider => new NancyControllerPathProvider();

        //protected override void ConfigureConventions(NancyConventions nancyConventions)
        //{
        //    // static content
        //    nancyConventions.StaticContentsConventions.Clear();
        //    nancyConventions.StaticContentsConventions.Add(
        //        StaticContentConventionBuilder.AddDirectory("Static", "Static"));

        //    // view location
        //    nancyConventions.ViewLocationConventions.Clear();
        //    nancyConventions.ViewLocationConventions.Add((viewName, model, context) =>
        //        String.Concat("Views/", viewName));
        //    nancyConventions.ViewLocationConventions.Add((viewName, model, context) =>
        //        String.Concat("Views/", context.ModuleName, "/", viewName));
        //}
    }
}
