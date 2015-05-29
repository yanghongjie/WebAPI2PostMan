using System;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using WebAPI2PostMan.Areas.HelpPage.ModelDescriptions;

namespace WebAPI2PostMan.Areas.HelpPage
{
    /// <summary>A custom <see cref="IDocumentationProvider"/> that reads the API documentation from a collection of XML documentation files.</summary>
    public class MultiXmlDocumentationProvider : IDocumentationProvider, IModelDocumentationProvider
    {
        /*********
        ** Properties
        *********/
        /// <summary>The internal documentation providers for specific files.</summary>
        private readonly XmlDocumentationProvider[] Providers;


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="paths">The physical paths to the XML documents.</param>
        public MultiXmlDocumentationProvider(params string[] paths)
        {
            this.Providers = paths.Select(p => new XmlDocumentationProvider(p)).ToArray();
        }

        /// <summary>Gets the documentation for a subject.</summary>
        /// <param name="subject">The subject to document.</param>
        public string GetDocumentation(MemberInfo subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }

        /// <summary>Gets the documentation for a subject.</summary>
        /// <param name="subject">The subject to document.</param>
        public string GetDocumentation(Type subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }

        /// <summary>Gets the documentation for a subject.</summary>
        /// <param name="subject">The subject to document.</param>
        public string GetDocumentation(HttpControllerDescriptor subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }

        /// <summary>Gets the documentation for a subject.</summary>
        /// <param name="subject">The subject to document.</param>
        public string GetDocumentation(HttpActionDescriptor subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }

        /// <summary>Gets the documentation for a subject.</summary>
        /// <param name="subject">The subject to document.</param>
        public string GetDocumentation(HttpParameterDescriptor subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }

        /// <summary>Gets the documentation for a subject.</summary>
        /// <param name="subject">The subject to document.</param>
        public string GetResponseDocumentation(HttpActionDescriptor subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Get the first valid result from the collection of XML documentation providers.</summary>
        /// <param name="expr">The method to invoke.</param>
        private string GetFirstMatch(Func<XmlDocumentationProvider, string> expr)
        {
            return this.Providers
                .Select(expr)
                .FirstOrDefault(p => !String.IsNullOrWhiteSpace(p));
        }
    }
}