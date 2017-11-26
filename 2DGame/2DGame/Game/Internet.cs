//#define Internet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

#if Internet
namespace Intro2DGame.Game
{
    class Internet
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void DllCallBack_SetPointer(int Index,int x, int y);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void DllCallBack_Shoot(int x, int y);

        [DllImport("GameInternet.dll", EntryPoint = "CreateBroadCast")]
        protected static extern bool CreateBroadCast();
        [DllImport("GameInternet.dll", EntryPoint = "ListenBroadCast")]
        protected static extern bool ListenBroadCast();
        [DllImport("GameInternet.dll", EntryPoint = "SetCallBackPlayer")]
        protected static extern bool SetCallBackPlayer(int Index,DllCallBack_SetPointer Proc);

        public Dictionary<int, SetPointerProc> MultiPlayerList;
        public int MultiPlayerListCount;
        public delegate void SetPointerProc(int x, int y);
        public Internet()
        {
            MultiPlayerListCount = 0;
            //MultiPlayerList = new Dictionary<int, SetPointerProc>();
        }
        public void CSharp_SetPointer(int Index,int x,int y)
        {
            MultiPlayerList[Index](x, y);
            /*
            foreach (int ListIndex in MultiPlayerList.Keys)
            {
                if (ListIndex == Index) MultiPlayerList[ListIndex](x, y);
            }
            */
        }
        public void AddMultiPlayer(int Index, SetPointerProc SetPointer)
        {
            DllCallBack_SetPointer PlayerPointerProc= new DllCallBack_SetPointer(CSharp_SetPointer);
            SetCallBackPlayer(Index,PlayerPointerProc);
            MultiPlayerList.Add(Index, SetPointer);
        }
    }
}
#endif