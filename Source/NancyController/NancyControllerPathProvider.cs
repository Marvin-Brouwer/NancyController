using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
