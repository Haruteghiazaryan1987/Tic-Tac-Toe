namespace Assets.Script.Advertisement
{
    public enum AdType
    {
        Interstitial,
        Rewarded
    }
    public interface IAdvertisement
    {
        void Show(AdType adType);
        void Load(AdType adType);
    }
}