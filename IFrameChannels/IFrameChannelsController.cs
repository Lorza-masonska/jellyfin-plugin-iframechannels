[HttpPost("Config")]
[Authorize] // Zmieniono z [Authorize(Policy = "RequiresElevation")]
public ActionResult SaveConfig([FromBody] PluginConfiguration newConfig)
{
    if (Plugin.Instance == null)
        return StatusCode(500);

    // Logowanie dla ułatwienia debugowania (zobaczysz to w logach Jellyfin)
    // _logger.LogInformation("Attempting to save IFrameChannels configuration");

    Plugin.Instance.Configuration.LibraryName = newConfig.LibraryName;
    Plugin.Instance.Configuration.Channels = newConfig.Channels;
    Plugin.Instance.SaveConfiguration();

    return Ok();
}