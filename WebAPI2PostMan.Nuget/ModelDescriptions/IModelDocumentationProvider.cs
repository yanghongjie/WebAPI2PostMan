using System;
using System.Reflection;

namespace WebAPI2PostMan.Nuget.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}