using PokeCore.BLL;
using PokeCore.DesktopUI;
using PokeCore.DTO;
using PokeCore.Utils;

namespace PokeCore
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new frmLogin());
        }
    }
}