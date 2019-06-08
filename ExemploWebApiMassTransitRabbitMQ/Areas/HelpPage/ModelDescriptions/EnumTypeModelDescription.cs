using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExemploWebApiMassTransitRabbitMQ.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}