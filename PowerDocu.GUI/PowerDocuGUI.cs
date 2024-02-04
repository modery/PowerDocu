using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerDocu.GUI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main(string[] args)
        {
            if (args is not { Length: > 0 })
            {
                //This  is required to ensure that the Form is run in STA mode
                Thread formsThread = new Thread(FormsMain)
                {
                    IsBackground = false,
                    Name = "PowerDocu Main"
                };
                formsThread.SetApartmentState(ApartmentState.STA);
                formsThread.Start();

            }
            //Args infers that this was executed from a shell
            else
            {
                await PowerDocuCLI.Run(args);
            }
        }

        static void FormsMain()
        {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PowerDocuForm());
        }
    }
}
