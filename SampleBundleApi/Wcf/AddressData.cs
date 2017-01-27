namespace SampleBundleApi.Wcf
{
    public struct AddressData
    {
        public AddressData(string baseAddress, string pipeName)
            : this()
        {
            this.BaseAddress = baseAddress;
            this.PipeName = pipeName;
        }

        public string BaseAddress { get; set; }
        public string PipeName { get; set; }
    }
}