namespace AdminPortal;

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
        public string API { get; init; }
    }

    public static string API
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(_rawConfig.API))
                return _rawConfig.API;
            else
                throw new Exception();
        }
    }
}