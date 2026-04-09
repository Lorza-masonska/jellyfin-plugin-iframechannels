using System.Collections.Generic;
using IFrameChannels.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IFrameChannels
{
    [ApiController]
    [Route("IFrameChannels")]
    [AllowAnonymous]
    public class IFrameChannelsController : ControllerBase
    {
        private readonly ILogger<IFrameChannelsController> _logger;

        public IFrameChannelsController(ILogger<IFrameChannelsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Channels")]
        public ActionResult<List<ChannelEntry>> GetChannels()
        {
            var config = Plugin.Instance?.Configuration;
            if (config == null)
                return Ok(new List<ChannelEntry>());
            return Ok(config.Channels);
        }

        [HttpGet("Config")]
        public ActionResult<PluginConfiguration> GetConfig()
        {
            var config = Plugin.Instance?.Configuration;
            if (config == null)
                return Ok(new PluginConfiguration());
            return Ok(config);
        }

        [HttpPost("Config")]
        public ActionResult SaveConfig([FromBody] PluginConfiguration newConfig)
        {
            if (Plugin.Instance == null)
                return StatusCode(500);
            Plugin.Instance.Configuration.LibraryName = newConfig.LibraryName;
            Plugin.Instance.Configuration.Channels = newConfig.Channels;
            Plugin.Instance.SaveConfiguration();
            return Ok();
        }
    }
}
