namespace SampleBundleApi.Wcf
{
    public interface IWcfClient<out T>
    {
        T GetChannel(string endPoint);
        event ConnectedEventHandler Connected;
    }
}