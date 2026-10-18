using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace CreamInstaller;

public class ProgramRelease
{
    private Asset asset;

    private string[] changes;

    private string commitHash;

    private bool commitHashParsed;

    private Version version;

    [JsonProperty("tag_name", NullValueHandling = NullValueHandling.Ignore)]
    public string TagName { get; set; }

    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    [JsonProperty("draft", NullValueHandling = NullValueHandling.Ignore)]
    public bool Draft { get; set; }

    [JsonProperty("prerelease", NullValueHandling = NullValueHandling.Ignore)]
    public bool Prerelease { get; set; }

    [JsonProperty("assets", NullValueHandling = NullValueHandling.Ignore)]
    public List<Asset> Assets { get; } = new();

    [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
    public string Body { get; set; }

    /// <summary>
    /// Release asset to download. Releases are published as a standalone executable; pre-releases
    /// use the CI executable named "CreamInstaller-CI{runNumber}-{commitHash}.exe".
    /// </summary>
    public Asset Asset => asset ??= Assets.FirstOrDefault(a =>
        Prerelease
            ? a.Name is not null
              && a.Name.StartsWith(Program.RepositoryPrereleasePrefix, StringComparison.OrdinalIgnoreCase)
              && a.Name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)
            : a.Name is not null
              && a.Name.Equals(Program.RepositoryExecutable, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Version parsed from the release tag (e.g. "v5.0.2.3" or "V5.2.0.0-CN-Fixed"). Any prefix
    /// (a leading "v"/"V") and suffix (everything from the first "-", e.g. "-CN-Fixed") is ignored.
    /// Null for pre-releases, which are identified by commit hash instead of a version number.
    /// </summary>
    public Version Version => version ??= ParseVersion(TagName);

    /// <summary>
    /// Commit hash of a pre-release (CI) build, parsed from its asset file name
    /// (e.g. "CreamInstaller-CI412-bcd29f4.exe" yields "bcd29f4"). Null for stable releases.
    /// </summary>
    public string CommitHash
    {
        get
        {
            if (!commitHashParsed)
            {
                commitHashParsed = true;
                string assetName = Asset?.Name;
                if (Prerelease && assetName is not null)
                {
                    string name = Path.GetFileNameWithoutExtension(assetName);
                    int separator = name.LastIndexOf('-');
                    if (separator != -1 && separator + 1 < name.Length)
                        commitHash = name[(separator + 1)..];
                }
            }

            return commitHash;
        }
    }

    public string[] Changes => changes ??= (Body ?? string.Empty).Replace("- ", "").Split("\r\n");

    private static System.Version ParseVersion(string tagName)
    {
        if (tagName is null)
            return null;
        string value = tagName;
        if (value.Length > 0 && (value[0] is 'v' or 'V'))
            value = value[1..];
        int separator = value.IndexOf('-');
        if (separator != -1)
            value = value[..separator];
        return System.Version.TryParse(value, out System.Version parsed) ? parsed : null;
    }
}

public class Asset
{
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
    public int Size { get; set; }

    [JsonProperty("browser_download_url", NullValueHandling = NullValueHandling.Ignore)]
    public string BrowserDownloadUrl { get; set; }
}
