namespace ScreenService
{
    partial class ControlScreen
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.stLapso = new System.Timers.Timer();
            this.spSensor = new System.IO.Ports.SerialPort(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.stLapso)).BeginInit();
            // 
            // stLapso
            // 
            this.stLapso.Enabled = true;
            this.stLapso.Elapsed += new System.Timers.ElapsedEventHandler(this.StLapso_Elapsed);
            // 
            // spSensor
            // 
            this.spSensor.BaudRate = 115200;
            //this.spSensor.PortName = "COM1";
            this.spSensor.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SpSensor_DataReceived);
            // 
            // ControlScreen
            // 
            this.ServiceName = "ControlScreen";
            ((System.ComponentModel.ISupportInitialize)(this.stLapso)).EndInit();

        }

        #endregion

        private System.Timers.Timer stLapso;
        private System.IO.Ports.SerialPort spSensor;
    }
}
