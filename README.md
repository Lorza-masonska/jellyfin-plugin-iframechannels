# IFrame Channels — Jellyfin Plugin

A Jellyfin plugin that lets you add custom iframe-based channels (streams, websites) as a library in your dashboard.

## Features

- Add any number of channels with custom names and URLs
- Channels appear as a tile grid (like a movie library)
- Click a channel to open it in a fullscreen iframe player
- Optional thumbnail image per channel
- Configurable library name (default: "Channels")
- Fully configurable from the Jellyfin dashboard

## Installation

### Option A — Manual

1. Download `IFrameChannels.zip` from [Releases](https://github.com/Lorza-masonska/jellyfin-plugin-iframechannels/releases)
2. Extract `IFrameChannels.dll` into your Jellyfin plugins folder:
   - Windows: `C:\ProgramData\Jellyfin\Server\plugins\IFrameChannels_1.0.0.0\`
   - Linux: `/var/lib/jellyfin/plugins/IFrameChannels_1.0.0.0/`
3. Restart Jellyfin

### Option B — Plugin Repository

1. In Jellyfin dashboard go to **Dashboard → Plugins → Repositories**
2. Add repository URL:
   ```
   https://raw.githubusercontent.com/Lorza-masonska/jellyfin-plugin-iframechannels/main/manifest.json
   ```
3. Go to **Catalog**, find **IFrame Channels**, install and restart

## Configuration

After installing, go to **Dashboard → Plugins → IFrame Channels**:

- Set the **library name** (shown in the sidebar)
- Add channels: provide a name, iframe URL, and optionally a thumbnail image URL
- Click **Save**

## Building from source

Requirements: [.NET 9 SDK](https://dotnet.microsoft.com/download)

```bash
dotnet build IFrameChannels/IFrameChannels.csproj --configuration Release
```

## License

MIT
