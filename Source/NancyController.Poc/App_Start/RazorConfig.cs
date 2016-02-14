using System.Collections.Generic;
using Nancy.ViewEngines.Razor;

namespace NancyController.Poc
{
    public class RazorConfig : IRazorConfiguration
    {
        public IEnumerable<string> GetAssemblyNames()
        {
            yield return "System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
            yield return "NancyController.Poc";
        }

        public IEnumerable<string> GetDefaultNamespaces()
        {
            yield return "NancyController.Poc";
        }

        public bool AutoIncludeModelNamespace => true;
    }
}
