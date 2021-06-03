using RpaInvesting.Controller;
using RpaInvesting.Utils;
using System;

namespace RpaInvesting
{
    class Program
    {
        private static readonly Logging log = new(@"C:\Robos\rpa.investing\log", "ativos-log-" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt");
        static void Main(string[] args)
        {
            log.Info("Iniciando aplicação");
            RpaController controller = new RpaController(log);
            controller.StartFlow();
            log.Info("Execução terminada");
        }
    }
}
