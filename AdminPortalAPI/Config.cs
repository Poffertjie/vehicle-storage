namespace AdminPortalAPI;

public static class Config
{
    public static RawConfig _rawConfig = null;
    public static void SetRawConfigInstance(RawConfig rawConfig)
    {
        if (_rawConfig == null)
            _rawConfig = rawConfig;
    }

    public class RawConfig
    {
        public string DB { get; init; }
        public string JwtKey { get; init; }
    }

    public static string DB
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(_rawConfig.DB))
                return _rawConfig.DB;
            else
                throw new Exception();
        }
    }

    public static string JwtKey
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(_rawConfig.JwtKey))
                return _rawConfig.JwtKey;
            else
                throw new Exception();
        }
    }
}