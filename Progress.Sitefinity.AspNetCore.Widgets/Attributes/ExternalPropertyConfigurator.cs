using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Progress.Sitefinity.AspNetCore.Widgets.Models.NativeChat;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Attributes
{
    /// <summary>
    /// The ExternalPropertyConfigurator attribute.
    /// </summary>
    internal class ExternalPropertyConfigurator : IPropertyConfigurator
    {
        private INativeChatClient nativeChatClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalPropertyConfigurator"/> class.
        /// </summary>
        /// <param name="nativeChatClient">The nativeChatClient parameter.</param>
        public ExternalPropertyConfigurator(INativeChatClient nativeChatClient)
        {
            this.nativeChatClient = nativeChatClient;
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
                var serializedChoices = this.FetchChoices();
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

        private string FetchChoices()
        {
            var bots = this.nativeChatClient.Bots().Result;
            var choices = new List<ChoiceValueDto>() { new ChoiceValueDto("Select", string.Empty) };

            foreach (var bot in bots)
            {
                choices.Add(new ChoiceValueDto(bot.DisplayName ?? bot.Name, bot.Id));
            }

            return JsonConvert.SerializeObject(choices);
        }
    }
}
