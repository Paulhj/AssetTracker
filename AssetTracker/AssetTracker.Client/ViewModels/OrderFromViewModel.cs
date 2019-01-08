namespace AssetTracker.Client.ViewModels
{
    public class OrderFromViewModel
    {
        public string Address { get; private set; } = string.Empty;

        public OrderFromViewModel(string address)
        {
            Address = address;
        }
    }
}
