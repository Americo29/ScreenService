using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput.Native;
using WindowsInput;
using System.Windows.Forms;

namespace ScreenService
{
    partial class ControlScreen : ServiceBase
    {
        bool blStateService = false;
        //bool flag1 = false;

        //InputSimulator sim = new InputSimulator();

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        //[DllImport("Kernel32.DLL", CharSet = CharSet.Auto, SetLastError = true)]
        //private extern static bool GetDevicePowerState(IntPtr hDevice, out bool fOn);

        //private static int MONITOR_ON = -1;
        //private static int MONITOR_OFF = 2;
        //private static int MONITOR_STANBY = 1;

        //private static IntPtr HWND_BROADCAST = new IntPtr(0xffff);
        //private static UInt32 WM_SYSCOMMAND = 0x0112;
        //private static IntPtr SC_MONITORPOWER = new IntPtr(0xF170);

        private int SC_MONITORPOWER = 0xF170;

        private uint WM_SYSCOMMAND = 0x0112;

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_MOVE = 0x0001;
        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;



        public ControlScreen()
        {
            InitializeComponent();
            
        }

        protected override void OnStart(string[] args)
        {
            // TODO: agregar código aquí para iniciar el servicio.
            stLapso.Start();
            EventLog.WriteEntry("inicio del servicio", EventLogEntryType.Information);
        }

        protected override void OnStop()
        {
            // TODO: agregar código aquí para realizar cualquier anulación necesaria para detener el servicio.
            stLapso.Stop();
            EventLog.WriteEntry("stop servicio", EventLogEntryType.Information);
        }

        private void StLapso_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
           // if (blStateService) return;      
            
            if(blStateService == false)
            {
                try
                {
                    SetMonitorState(MonitorState.OFF);
                    blStateService = true;

                    /*                  
                    Thread.Sleep(1000);
                    NativeMethods.MonitorOn();
                    EventLog.WriteEntry("monitor encendido", EventLogEntryType.Information);   
                    */
                    //SystemEvents.PowerModeChanged += OnPowerModeChange;     
                }                catch (Exception ex)
                {
                    EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
                }
            }
            else
            {
                SetMonitorState(MonitorState.ON);
                MouseWake();
            }
        }
        public void MouseWake()
        {
            mouse_event(MOUSEEVENTF_MOVE, 0, 1, 0, 0);
            Thread.Sleep(40);
            mouse_event(MOUSEEVENTF_MOVE, 0, -1, 0, 0);
            /*
            int x = 100;
            int y = 100;

            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
            */
        }

        enum MonitorState
        {
            ON = -1,
            OFF = 2,
            STANDBY = 1
        }
        private void SetMonitorState(MonitorState state)
        {
            Form frm = new Form();

            SendMessage(frm.Handle, WM_SYSCOMMAND, (IntPtr)SC_MONITORPOWER, (IntPtr)state);

        }


        private static class NativeMethods
        {
            internal static void MonitorOn()
            {
                //SendMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, (IntPtr)MONITOR_ON);
                //Console.WriteLine("Restaurando sesion");
            }

            internal static void MonitorOff()
            {
                //SendMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, (IntPtr)MONITOR_OFF);
            }
        }

        
         
            private void OnPowerModeChange(object sender, PowerModeChangedEventArgs e)
            {
                switch (e.Mode)
                {
                    case PowerModes.Resume:
                        EventLog.WriteEntry("pantalla encendida", EventLogEntryType.Information);
                        break;
                    case PowerModes.Suspend:
                        EventLog.WriteEntry("estado suspendido de pantalla", EventLogEntryType.Information);                  
                        //Thread.Sleep(1000);                 
                        try
                        {                    
                            EventLog.WriteEntry("tratando de iniciar screen", EventLogEntryType.Information);
                        }
                        catch(Exception ex)
                        {
                            EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
                            EventLog.WriteEntry("errorEERROORR", EventLogEntryType.Information);
                        }  
                        break;
                    case PowerModes.StatusChange:
                        EventLog.WriteEntry("status change", EventLogEntryType.Information);
                        break;

                }
            }
            
        }
}
