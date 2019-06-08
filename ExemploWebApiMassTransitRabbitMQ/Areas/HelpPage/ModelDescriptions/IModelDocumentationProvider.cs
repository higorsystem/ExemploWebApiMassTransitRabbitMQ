using System;
using System.Reflection;

namespace ExemploWebApiMassTransitRabbitMQ.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}