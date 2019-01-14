using System.Collections.Generic;

namespace AssetTracker.Client.ViewModels
{
    public class AssetIndexViewModel
    {
        public IEnumerable<Model.Asset> Assets { get; private set; }
            = new List<Model.Asset>();

        public AssetIndexViewModel(List<Model.Asset> assets)
        {
            Assets = assets;
        }
    }

    
}
