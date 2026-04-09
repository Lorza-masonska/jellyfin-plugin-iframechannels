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
            // Pobieramy aktualną konfigurację z instancji pluginu
            var config = Plugin.Instance?.Configuration;
            if (config == null) return Ok(new PluginConfiguration());
            return Ok(config);
        }

        [HttpPost("Config")]
        [Authorize]
        public ActionResult SaveConfig([FromBody] PluginConfiguration newConfig)
        {
            if (Plugin.Instance == null) return StatusCode(500);
            if (newConfig == null) return BadRequest("No configuration provided");

            try 
            {
                // Aktualizujemy dane w pamięci
                Plugin.Instance.Configuration.LibraryName = newConfig.LibraryName;
                Plugin.Instance.Configuration.Channels = newConfig.Channels ?? new List<ChannelEntry>();
                
                // Zapisujemy do pliku xml
                Plugin.Instance.SaveConfiguration();
                
                _logger.LogInformation("IFrameChannels: Configuration saved successfully.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "IFrameChannels: Error saving configuration.");
                return StatusCode(500, ex.Message);
            }
        }
    }
}