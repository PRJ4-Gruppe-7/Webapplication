using System.Collections.Generic;
using web_app_backend.Models;

namespace web_app_backend
{
    public interface IController
    {
        void Receive();
        void OpenConnection();
        void CloseConnection();
        void JsonSerialize(List<Heatmap> dataHeatmaps);
    }
}