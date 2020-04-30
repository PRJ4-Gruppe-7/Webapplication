using System.Collections.Generic;

namespace web_app_backend2_API.Interfaces
{
    public interface ICheckEachString
    {
        void CheckString(string[] line, List<string> line1, List<string> line2, List<string> line3);
    }
}