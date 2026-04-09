using System.Collections.Generic;
using IFrameChannels.Configuration;
using MediaBrowser.Controller.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IFrameChannels
{
    [ApiController]
    [Route("IFrameChannels")]
    public class IFrameChannelsController : ControllerBase
    {
        private readonly ILogger<IFrameChannelsController> _logger;

        public IFrameChannelsController(ILogger<IFrameChannelsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Channels")]
        [Authorize]
        public ActionResult<List<ChannelEntry>> GetChannels()
        {
            var config = Plugin.Instance?.Configuration;
            return Ok(config?.Channels ?? new List<ChannelEntry>());
        }

        [HttpGet("Config")]
        [Authorize]
        public ActionResult<PluginConfiguration> GetConfig()
        {
            var config = Plugin.Instance?.Configuration;
            return Ok(config ?? new PluginConfiguration());
        }

        [HttpPost("Config")]
        [Authorize] // Zmienione z RequiresElevation na zwykłe Authorize dla testu
        public ActionResult SaveConfig([FromBody] PluginConfiguration newConfig)
        {
            if (Plugin.Instance == null) return StatusCode(500);

            Plugin.Instance.Configuration.LibraryName = newConfig.LibraryName;
            Plugin.Instance.Configuration.Channels = newConfig.Channels;
            Plugin.Instance.SaveConfiguration();

            return Ok();
        }
    }
}