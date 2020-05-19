using System.Collections.Generic;
using web_app_backend2.Models;

namespace web_app_backend2_API.Interfaces
{
    public interface ICompareLists
    {
        void CompareList(List<Heatmap> heatmap, List<Heatmap> Reference);
    }
}