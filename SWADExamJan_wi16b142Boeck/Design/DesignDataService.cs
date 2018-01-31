using System;
using SWADExamJan_wi16b142Boeck.Model;

namespace SWADExamJan_wi16b142Boeck.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data

            var item = new DataItem("Welcome to MVVM Light [design]");
            callback(item, null);
        }
    }
}