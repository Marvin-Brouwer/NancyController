using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Nancy;
using Nancy.Responses.Negotiation;

namespace NancyController
{
    /// <summary>
    /// A NancyController Creates a Nancy module but looks like an Mvc.Net controller
    /// </summary>
    public abstract class NancyControllerModule : NancyModule
    {
        protected List<Type> ExcludeTypesFromsResolving = new List<Type>()
        {
            { typeof(NancyModule) },
            { typeof(NancyControllerModule) },
        };
        /// <summary>
        /// A NancyController Creates a Nancy module but looks like an Mvc.Net controller
        /// </summary>
        protected NancyControllerModule() : base()
        {
            ModulePath = GetType()
                .Name.Substring(0, GetType().Name.LastIndexOf("module", StringComparison.OrdinalIgnoreCase));
            if (ModulePath.Equals("home", StringComparison.OrdinalIgnoreCase))
            {
                ModulePath = "/";
                // todo: find a way out to use '/' and '/Home'
            }

            Initialize();
        }
        /// <summary>
        /// A NancyController Creates a Nancy module but looks like an Mvc.Net controller
        /// </summary>
        /// <param name="modulePath">Override the modulePath, usually this is resolved by the controllerName</param>
        protected  NancyControllerModule(string modulePath) : base(modulePath)
        {
            Initialize();
        }

        /// <summary>
        /// Iterate through all the method's returning any type of Nancy Response
        /// </summary>
        protected virtual void Initialize()
        {
            // Did you really mean to prohibit public methods? I assume not
            var methods = GetType().GetMethods(
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Instance);
            var retMethods = methods
                .Where(m => ExcludeTypesFromsResolving.Any(etr => m.DeclaringType != etr))
                .Where(m =>
                    typeof(Response).IsAssignableFrom(m.ReturnType) ||
                    typeof(Negotiator).IsAssignableFrom(m.ReturnType)
                );
            foreach (var method in retMethods)
            {
                var attributes = method.GetCustomAttributes().ToArray();
                if (!attributes.Any() || attributes.Any(a => a is HttpGetAttribute))
                {
                    if (method.Name.Equals("index", StringComparison.OrdinalIgnoreCase))
                        CreateGetMethod(method);
                    CreateGetMethod(method, method.Name);
                }
                if (attributes.Any(a => a is HttpPostAttribute))
                {
                    if (method.Name.Equals("index", StringComparison.OrdinalIgnoreCase))
                        CreatePostMethod(method);
                    CreatePostMethod(method, method.Name);
                }
            }
        }

        /// <summary>
        /// Register a Get Method to the Nancy Module
        /// </summary>
        /// <param name="method">The actual Method</param>
        /// <param name="path">The Path assigned to this method</param>
        private void CreateGetMethod(MethodInfo method, string path = null)
        {
            var methodPath = String.IsNullOrWhiteSpace(path) ? String.Empty : path;
            var parameters = method.GetParameters();
            if (!parameters.Any())
            {
                Get[$"/{methodPath}"] = x =>
                    method.Invoke(this, null);
            }
            else
            {
                Get[$"/{methodPath}{BuildParameterPath(method.GetParameters())}"] = x =>
                    method.Invoke(this, ExtractParameExtractParamtersFromPath(x, parameters));
            }
        }
        /// <summary>
        /// Register a Post Method to the Nancy Module
        /// </summary>
        /// <param name="method">The actual Method</param>
        /// <param name="path">The Path assigned to this method</param>
        private void CreatePostMethod(MethodInfo method, string path = null)
        {
            var methodPath = String.IsNullOrWhiteSpace(path) ? String.Empty : path;
            var parameters = method.GetParameters();
            if (!parameters.Any())
            {
                Post[$"/{methodPath}"] = x =>
                    method.Invoke(this, null);
            }
            else
            {
                Post[$"/{methodPath}{BuildParameterPath(method.GetParameters())}"] = x =>
                    method.Invoke(this, ExtractParameExtractParamtersFromPath(x, parameters));
            }
        }

        private static dynamic[] ExtractParameExtractParamtersFromPath(dynamic x, ParameterInfo[] parameters)
        {
            var nancyParameters = (DynamicDictionary) x;
            var reqParams = parameters.Select(pi =>
            {
                var parameter = nancyParameters[pi.Name];
                return Convert.ChangeType(parameter, pi.ParameterType);
            }).ToArray();
            return reqParams;
        }

        private static string BuildParameterPath(ParameterInfo[] parameters)
        {
            if(!parameters.Any()) return String.Empty;
            var parameterPath = Path.Combine(parameters.Select(p => $"{{{p.Name}}}").ToArray());
            return $"/{parameterPath}";
        }
    }
}
