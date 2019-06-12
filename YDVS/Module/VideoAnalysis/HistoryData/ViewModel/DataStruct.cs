using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoAnalysis.HistoryData.ViewModel
{
    public struct DataStruct
    {
        /** 开始标识 0xAAAA */
        public UInt16 head;
        /** 报文长度 0x60 */
        public UInt16 length;
        /** 报文类型 0x07 */
        public Byte type;
        /** 时间  */
        public Timestruct Ttmestruct;
        /** 中央处理平台车号 */
        public TrainNumStruct tinNumStruct;
        /** 标志  */
        public Byte tax;
        /** 预留  */
        public UInt32 tax_time;
        /** 车次部分  */
        public TrainNumbStruct tainNumbStruct;
        /**车站号 */
        public UInt16 stationnum;
        /**司机号 */
        public UInt16 driverNum;
        /**副司机号 */
        public UInt16 assistentDriverNum;
        /**预留 */
        public UInt16 car_num;
        /**预留 */
        public Byte car_type;
        /**预留 */
        public Byte car_type_ex;
        /**实际交路号 */
        public Byte crossroads;
        /**客/货、本/补 */
        public Byte data10;
        /**速度 */
        public Byte speed;
        /**预留 */
        public UInt16 speedres;
        /**机车信号 */
        public Byte signal;
        /**机车工况 */
        public Byte statu;
        /**信号机编号 */
        public UInt16 signalnum;
        /**信号机种类 */
        public UInt16 signaltype;
        /**公里标 */
        public UInt16 kilometer_post;
        /**总重 */
        public UInt16 totalWeight;
        /**计长 */
        public UInt16 counterlength;
        /**辆数 */
        public Byte trainnum;
        /**列车管压力 */
        public UInt16 pipepressure;
        /**装置状态 */
        public Byte devicestatu;
        /**TCMS报文有效 */
        public Byte tcmsmessage;
        /**司机室状态 */
        public Byte driverroomstatu;
        /**预留字节 */
        public Byte jcgk;
        /**受电弓状态（b1～b0:一端受电弓，b3～b2:二端受电弓，0-无效，1-升弓，2-降弓，3-隔离）*/
        public Byte pantograph_statu;
        /**主断状态 （1-断开，2-闭合，0xFF-无效）*/
        public Byte mainfault_statu;
        /**手柄级位 （×0.1级,0xFFFF无效）*/
        public UInt16 Handle_level;
        /**重联信息 （1-重联，2-不重联，0xFF-无效）*/
        public Byte reconect_statu;
        /**大闸指令 （b0-运转位，b1-初制动，b2-常用制动区，b3-全制动，b4-抑制位，b5-重联位，b6-紧急制动位，0xFF-无效）*/
        public Byte sluice_command;
        /**小闸指令 （b0-运转位，b1-制动区，b2-全制动，0xFF-无效）*/
        public Byte brake_command;
        /**其他指令 （位定义：1表示有效，0表示无效）*/
        public Byte other_command;
        /**其他指令屏蔽字节（位定义：1表示有效，0表示无效）*/
        public Byte other_command1;
        /**预留字节*/
        public UInt16 ZDJ;
        /**预留字节*/
        public UInt16 ZDG;
        /**预留字节*/
        public UInt16 ZFG;
        /**预留字节*/
        public Byte av31;
        /**预留字节*/
        public Byte av32;
        /**预留字节*/
        public Byte av33;
        /**预留字节*/
        public Byte av34;
        /**预留字节*/
        public Byte av35;
        /**校验*/
        public Byte src;
    }
}
