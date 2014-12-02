using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace YGJZJL.CarSip.Client.Meas
{
    public interface IHkClientSink
    {
        int OnPosLength(UInt32 nLength);
        int OnPresentationOpened(int success);
        int OnPresentationClosed();
        int OnPreSeek(UInt32 uOldTime, UInt32 uNewTime);
        int OnPostSeek(UInt32 uOldTime, UInt32 uNewTime);
        int OnStop();
        int OnBegin(UInt32 uTime);
        int OnRandomBegin(UInt32 uTime);
        int OnContacting(string pszHost);
        int OnPutErrorMsg(string pError);
        int OnBuffering(UInt32 uFlag, UInt16 uPercentComplete);
        int OnChangeRate(int flag);
        int OnDisconnect();
    };

   public delegate int DataReceivedEventHandle(int sid, int iusrdata, int idatatype, IntPtr  pdata, int ilen);
   public delegate int MessageBackEventHandle(int sid, int opt, int param1, int param2);
   public  class HkClient
   {
       #region 
       [DllImport("client.dll", SetLastError = true)]
        public static extern int InitStreamClientLib();
        [DllImport("client.dll")]
        public static extern int FiniStreamClientLib();
        [DllImport("client.dll", SetLastError = true)]
        public static extern int HIKS_CreatePlayer(IHkClientSink pSink, IntPtr pWndSiteHandle, DataReceivedEventHandle pDataRec, MessageBackEventHandle pMsgFunc, int TransMethod);
        [DllImport("client.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int HIKS_OpenURL(int hSession, string pszURL, int iusrdata);
        //public static extern int HIKS_OpenURL(int hSession, byte[] pszURL, int iusrdata);
        [DllImport("client.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int HIKS_Play(int hSession);
        [DllImport("client.dll")]
        public static extern int HIKS_RandomPlay(int hSession, int timepos);
        [DllImport("client.dll")]
        public static extern int HIKS_Pause(int hSession);
        [DllImport("client.dll")]
        public static extern int HIKS_Resume(int hSession);
        [DllImport("client.dll")]
        public static extern int HIKS_Stop(int hSession);
        [DllImport("client.dll")]
        public static extern int HIKS_GetCurTime(int hSession, ref ushort uTime);
        [DllImport("client.dll")]
        public static extern int HIKS_ChangeRate(int hSession, int H_scale);
        [DllImport("client.dll")]
        public static extern int HIKS_Destroy(int hSession);
        [DllImport("client.dll")]
        public static extern int HIKS_GetVideoParams(int hSession, ref  int ibri, ref int icon, ref int isat, ref int ihue);
        [DllImport("client.dll")]
        public static extern int HIKS_SetVideoParams(int hSession, int ibri, int icon, int isat, int ihue);
        [DllImport("client.dll")]
        public static extern int HIKS_PTZControl(int hSession, ushort ucommand, int iparaml, int iparam2, int iparam3, int iparam4);
        [DllImport("client.dll")]
        public static extern int HIKS_SetVolume(int hSession, ushort volume);
        [DllImport("client.dll")]
        public static extern int HIKS_OpenSound(int hSession, bool bExclusive);
        [DllImport("client.dll")]
        public static extern int HIKS_CloseSound(int hSession);
        [DllImport("client.dll")]
        public static extern int HIKS_ThrowBFrameNum(int hSession, uint nNum);
        [DllImport("client.dll")]
        public static extern int HIKS_GrabPic(int hSession, string szPicFileName, ushort byPicType);
       #endregion   
   }
}
