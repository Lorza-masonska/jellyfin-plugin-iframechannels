using System;
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

        [HttpGet("Config")]
        [Authorize]
        public ActionResult<PluginConfiguration> GetConfig()
        {
            var config = Plugin.Instance?.Configuration;
            return Ok(config ?? new PluginConfiguration());
        }

        [HttpPost("Config")]
        [Authorize] // Zmienione z RequiresElevation dla testu
        public ActionResult SaveConfig([FromBody] PluginConfiguration newConfig)
        {
            if (Plugin.Instance == null) return StatusCode(500);
            if (newConfig == null) return BadRequest("No config sent");

            try 
            {
                _logger.LogInformation("IFrameChannels: Saving new configuration...");
                
                // Ręczne przypisanie, aby mieć pewność, że obiekt nie jest nullem
                Plugin.Instance.Configuration.LibraryName = newConfig.LibraryName ?? "Channels";
                Plugin.Instance.Configuration.Channels = newConfig.Channels ?? new List<ChannelEntry>();
                
                Plugin.Instance.SaveConfiguration();
                
                _logger.LogInformation("IFrameChannels: Configuration saved to XML.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "IFrameChannels: Save failed");
                return StatusCode(500, ex.Message);
            }
        }
    }
}