using Services.Shared.Models;
using System.Collections.Generic;

namespace Services.Data
{
    public interface ISiteRepository
    {
        IEnumerable<Site> GetSites();

        Sample GetLastSample();
    }
}