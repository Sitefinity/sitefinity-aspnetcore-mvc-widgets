using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Progress.Sitefinity.AspNetCore.Widgets.Models.NativeChat;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Attributes
{
    /// <summary>
    /// The ExternalPropertyConfigurator attribute.
    /// </summary>
    internal class ExternalPropertyConfigurator : IPropertyConfigurator
    {
        public const string ChoicesProviderNativeChat = "NativeChatClient";
        public const string ChoicesProviderSitefinityAssistant = "SitefinityAssistantClient";
        private readonly Dictionary<string, IExternalChoicesProvider> registeredProviders;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalPropertyConfigurator"/> class.
        /// </summary>
        /// <param name="choicesProviders">The providers to use when cofigurating properties.</param>
        public ExternalPropertyConfigurator(IEnumerable<IExternalChoicesProvider> choicesProviders)
        {
            this.registeredProviders = new Dictionary<string, IExternalChoicesProvider>();

            foreach (var provider in choicesProviders)
            {
                this.registeredProviders.Add(provider.Name, provider);
            }
        }

        /// <inheritdoc/>
        public virtual void ProcessPropertyMetadataContainer(PropertyDescriptor descriptor, PropertyMetadataContainerDto propertyContainer, string componentName)
        {
            foreach (Attribute attr in descriptor?.Attributes)
            {
                if (propertyContainer != null)
                    this.ProcessConfigurationExternalDataChoiceAttribute(attr, propertyContainer);
            }
        }

        private void ProcessConfigurationExternalDataChoiceAttribute(Attribute attribute, PropertyMetadataContainerDto propertyContainer)
        {
            var externalChoiceAttr = attribute as ExternalDataChoiceAttribute;
            if (externalChoiceAttr != null)
            {
                var serializedChoices = this.FetchChoices(externalChoiceAttr.ChoicesProviderName);
                propertyContainer.Properties.Add($"{WidgetMetadataConstants.Prefix}_Choices", serializedChoices);

                var choiceKey = $"{WidgetMetadataConstants.Prefix}_Choice_Choices";
                if (propertyContainer.Properties.ContainsKey($"{WidgetMetadataConstants.Prefix}_Choice_Choices"))
                {
                    propertyContainer.Properties[choiceKey] = serializedChoices;
                }
                else
                {
                    propertyContainer.Properties.Add(choiceKey, serializedChoices);
                }
            }
        }

        private string FetchChoices(string providerName)
        {
            var provider = this.registeredProviders[providerName];
            var choices = new List<ChoiceValueDto>() { new ChoiceValueDto("Select", string.Empty) };
            var providedChoices = Task.Factory.StartNew(provider.FetchChoicesAsync).Unwrap().GetAwaiter().GetResult();

            foreach (var choice in providedChoices)
            {
                choices.Add(choice);
            }

            return JsonConvert.SerializeObject(choices);
        }
    }
}
