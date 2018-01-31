using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWADExamJan_wi16b142Boeck.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}
