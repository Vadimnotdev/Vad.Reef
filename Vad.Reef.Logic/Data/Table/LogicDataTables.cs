using System.Data;
using Vad.Reef.Logic.Data;
using Vad.Reef.Logic.Data.Table;
using Vad.Reef.Titan.Logic.CSV;
using Vad.Reef.Titan.Logic.Debug;

namespace Vad.Reef.Logic.Data.Tables
{

    public class LogicDataTables
    {
        private static LogicDataTable[] _tables = new LogicDataTable[30];

        private const int COUNT = 52;

        public static LogicData? GetDataById(int id)
        {
            int classID = GlobalID.GetClassId(id) - 1;

            if (classID >= 0 && classID < COUNT && _tables[classID] != null)
            {
                return _tables[classID].GetItemById(id);
            }
            return null;
        }
    }
}