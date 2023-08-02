using Ardalis.SmartEnum;

namespace Domain.Enumerations;

public class BannerStatus : SmartEnum<BannerStatus>
{
    private BannerStatus(string name, int value)
        : base(name, value) { }
    
    public static readonly BannerStatus Inactive = new InactiveStatus();
    public static readonly BannerStatus Active = new ActiveStatus();

    public static implicit operator BannerStatus(string name)
        => FromName(name);

    public static implicit operator BannerStatus(int value)
        => FromValue(value);

    public static implicit operator string(BannerStatus status)
        => status.Name;

    public static implicit operator int(BannerStatus status)
        => status.Value;
    
    private class InactiveStatus : BannerStatus
    {
        public InactiveStatus()
            : base(nameof(Inactive), 0) { }
    }
    
    private class ActiveStatus : BannerStatus
    {
        public ActiveStatus()
            : base(nameof(Active), 1) { }
    }
    
}