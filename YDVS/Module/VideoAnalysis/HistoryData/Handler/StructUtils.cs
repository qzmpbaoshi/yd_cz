using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoAnalysis.HistoryData.ViewModel;

namespace VideoAnalysis.HistoryData.Handler
{
    public class StructUtils
    {
        private static  int IS_DCUDRBEGIN = 0;
        private static readonly int IS_DCUDREND = -1;
        public static Boolean isValidDataStruct(UInt16 type_flag, int i)
        {
            String typeFlag = getTypeFlag(type_flag);
            switch (i)
            {
                case 0:
                    if (typeFlag.Equals("0xAAAA"))
                    {
                        return true;
                    }
                    break;
                
            }
            return false;
        }
        private static String getTypeFlag(int typeFlag)
        {
            return String.Format("0x%04x", typeFlag);
        }

        public static DataStruct? GetDataStruct(byte[] data)
        {
            //DataStruct dataStruct = new DataStruct();
            DataStruct byteToStuct = StructHelper.ByteToStuct<DataStruct>(data);
            if (data.Length== byteToStuct.length && isValidDataStruct(byteToStuct.head,0) )
            {
                return byteToStuct;
            }
            return null;
        }
    }

}
