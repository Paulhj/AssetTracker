using System.Collections.Generic;

namespace AssetTracker.Client.ViewModels
{
    public class AssetsViewModel
    {
        public IEnumerable<Model.Asset> Assets { get; private set; }
            = new List<Model.Asset>();

        public AssetsViewModel(List<Model.Asset> assets)
        {
            Assets = assets;
        }
    }

    
}
