using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace CreamInstaller.Utility;

internal static class Locale
{
    private static readonly Dictionary<string, string> Strings = new();
    private static readonly Dictionary<string, string> Fallback = new();

    internal static string Current { get; private set; } = "en-US";

    internal static void Load(string language)
    {
        Current = language;
        Strings.Clear();
        foreach ((string key, string value) in LoadLanguage(language))
            Strings[key] = value;

        Fallback.Clear();
        if (language != "en-US")
            foreach ((string key, string value) in LoadLanguage("en-US"))
                Fallback[key] = value;
    }

    private static Dictionary<string, string> LoadLanguage(string language)
    {
        string resourceName = $"CreamInstaller.Resources.Languages.{language}.json";
        using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        if (stream is null)
            return new Dictionary<string, string>();
        using StreamReader reader = new(stream);
        string json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
    }

    internal static string Get(string key) =>
        Strings.TryGetValue(key, out string value) ? value
        : Fallback.TryGetValue(key, out string fallback) ? fallback
        : key;

    internal static string Format(string key, params object[] args)
        => string.Format(CultureInfo.CurrentCulture, Get(key), args);
}
