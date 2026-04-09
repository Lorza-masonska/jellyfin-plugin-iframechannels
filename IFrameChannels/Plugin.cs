using System;
using System.Collections.Generic;
using IFrameChannels.Configuration;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;

namespace IFrameChannels
{
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {
        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
            : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        public override string Name => "IFrame Channels";

        public override Guid Id => Guid.Parse("a2b3c4d5-e6f7-8901-abcd-ef1234567890");

        public override string Description => "Add custom iframe channels as a library in Jellyfin.";

        public static Plugin? Instance { get; private set; }

        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = "IFrameChannels",
                    EmbeddedResourcePath = $"{GetType().Namespace}.Web.config.html",
                    EnableInMainMenu = false
                },
                new PluginPageInfo
                {
                    Name = "IFrameChannelsView",
                    EmbeddedResourcePath = $"{GetType().Namespace}.Web.channels.html",
                    EnableInMainMenu = true,
                    DisplayName = Instance?.Configuration.LibraryName ?? "Channels",
                    MenuSection = "library",
                    MenuIcon = "live_tv"
                }
            };
        }
    }
}
