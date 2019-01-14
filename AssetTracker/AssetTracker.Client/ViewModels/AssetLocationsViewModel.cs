using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracker.Client.ViewModels
{
    public class AssetLocationsViewModel
    {
        public IEnumerable<Model.AssetLocation> AssetLocations { get; private set; }
           = new List<Model.AssetLocation>();

        public AssetLocationsViewModel(List<Model.AssetLocation> assetLocations)
        {
            AssetLocations = assetLocations;
        }
    }
}
