using System;
using System.IO;
using Nancy;

namespace NancyController
{
    public class NancyControllerPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            // Move up from /bin/{target}
            return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\"));
        }
    }
}
