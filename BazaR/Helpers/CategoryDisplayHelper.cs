namespace BazaR.Helpers;

public static class CategoryDisplayHelper
{
    private static readonly string[] ImageExtensions =
    [
        ".svg", ".png", ".jpg", ".jpeg", ".webp", ".gif", ".jfif", ".bmp", ".ico"
    ];

    /// <summary>
    /// True when IconUrl/ImgUrl should be rendered as &lt;img src&gt;.
    /// Requires an absolute web path (/...) or http(s) URL so relative file names are not mistaken for images
    /// (they were historically stored as Bootstrap icon suffixes).
    /// </summary>
    public static bool IsWebImagePath(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;

        var u = url.Trim();
        if (u.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            u.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            return true;

        if (!u.StartsWith('/'))
            return false;

        if (u.Contains("AssetsIconImg", StringComparison.OrdinalIgnoreCase))
            return true;

        foreach (var ext in ImageExtensions)
        {
            if (u.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }
}