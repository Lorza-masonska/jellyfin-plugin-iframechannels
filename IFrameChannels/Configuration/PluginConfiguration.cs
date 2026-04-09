using System.Collections.Generic;
using MediaBrowser.Model.Plugins;

namespace IFrameChannels.Configuration
{
    public class ChannelEntry
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
    }

    public class PluginConfiguration : BasePluginConfiguration
    {
        public string LibraryName { get; set; } = "Channels";
        public List<ChannelEntry> Channels { get; set; } = new List<ChannelEntry>();
    }
}
