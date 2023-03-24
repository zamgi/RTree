using System;
using System.Windows.Forms;

namespace trees.win_forms
{
    /// <summary>
    /// 
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///
        /// </summary>
        [STAThread] private static void Main( string[] args )
        {
            Application.ThreadException                  += (sender, e) => MessageBox.Show( e.Exception      .ToString(), "Application.ThreadException"               , MessageBoxButtons.OK, MessageBoxIcon.Error );
            AppDomain  .CurrentDomain.UnhandledException += (sender, e) => MessageBox.Show( e.ExceptionObject.ToString(), "AppDomain.CurrentDomain.UnhandledException", MessageBoxButtons.OK, MessageBoxIcon.Error );
            Application.SetUnhandledExceptionMode( UnhandledExceptionMode.Automatic, true );

#if NET5_0_OR_GREATER
            Application.SetHighDpiMode( HighDpiMode.SystemAware );
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new RTreeForm() );
        }
    }
}
