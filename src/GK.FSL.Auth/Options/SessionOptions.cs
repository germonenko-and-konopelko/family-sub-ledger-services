namespace GK.FSL.Auth.Options;

public class SessionOptions
{
    public TimeSpan StaleSessionCleanupInterval { get; set; }

    public TimeSpan IdleTimespan { get; set; }
}