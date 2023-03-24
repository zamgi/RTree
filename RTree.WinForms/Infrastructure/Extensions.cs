using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;

using trees.win_forms.Properties;

using _Node_ = System.Collections.Generic.RTree< trees.win_forms.RTreeRECT >.Node;
using M = System.Runtime.CompilerServices.MethodImplAttribute;
using O = System.Runtime.CompilerServices.MethodImplOptions;

namespace trees.win_forms
{
    /// <summary>
    /// 
    /// </summary>
    internal static class Extensions
    {
        public static Size GetBoundRectSize( this IReadOnlyList< _Node_ > nodes )
        {
            var min_x = float.MaxValue;
            var min_y = float.MaxValue;
            var max_x = float.MinValue;
            var max_y = float.MinValue;
            foreach ( var n in nodes )
            {
                ref readonly var e = ref n.Envelope;
                min_x = MathF.Min( min_x, e.Min_X );
                min_y = MathF.Min( min_y, e.Min_Y );
                max_x = MathF.Max( max_x, e.Max_X );
                max_y = MathF.Max( max_y, e.Max_Y );
            }

            var size = new Size( (int) (max_x + min_x + 0.5f), (int) (max_y + min_y + 0.5f) );
            return (size);
        }
        [M(O.AggressiveInlining)] public static Rectangle ToRectangle( in this Envelope e ) => new Rectangle( (int) e.Min_X, (int) e.Min_Y, (int) e.Width, (int) e.Height );
        [M(O.AggressiveInlining)] public static Rectangle ToRectangle( in this Envelope e, int height )
        {
            var x = e.Min_X;
            var y = height - e.Max_Y; //e.MinY;
            var w = e.Max_X - e.Min_X;
            var h = e.Max_Y - e.Min_Y;
            return (new Rectangle( (int) x, (int) y, (int) w, (int) h ));
        }
        [M(O.AggressiveInlining)] public static (int x, int y, int w, int h) ToChoords( in this Envelope e, int height )
        {
            var x = e.Min_X;
            var y = height - e.Max_Y; //e.MinY;
            var w = e.Max_X - e.Min_X;
            var h = e.Max_Y - e.Min_Y;
            return ((int) x, (int) y, (int) w, (int) h);
        }
        [M(O.AggressiveInlining)] public static Envelope ToEnvelope( in this Rectangle r ) => new Envelope( r.X, r.Y, r.Right, r.Bottom );
        [M(O.AggressiveInlining)] public static Envelope ToEnvelope( in this Rectangle r, int height, in Point scroll_pt )
        {
            var y = height - r.Bottom;
            var env = new Envelope( r.X           - scroll_pt.X, y            + scroll_pt.Y, 
                                    r.X + r.Width - scroll_pt.X, y + r.Height + scroll_pt.Y );
            return (env);
        }

        [M(O.AggressiveInlining)] public static Circle ToCircle_Outscribed( in this Rectangle r ) => r.ToEnvelope().ToCircle_Outscribed();

        [M(O.AggressiveInlining)] public static bool IsNullOrEmpty( this string s ) => string.IsNullOrEmpty( s );
        [M(O.AggressiveInlining)] public static bool IsNullOrWhiteSpace( this string s ) => string.IsNullOrWhiteSpace( s );
        [M(O.AggressiveInlining)] public static bool HasFirstCharNotDot( this string s ) => (s != null) && (0 < s.Length) && (s[ 0 ] != '.');
        [M(O.AggressiveInlining)] public static bool AnyEx< T >( this IEnumerable< T > seq ) => (seq != null) && seq.Any();
        [M(O.AggressiveInlining)] public static bool AnyEx< T >( this IReadOnlyList< T > seq ) => (seq != null) && (0 < seq.Count);
        
        [M(O.AggressiveInlining)] public static T? Try2Enum< T >( this string s ) where T : struct => (Enum.TryParse< T >( s, true, out var t ) ? t : (T?) null);
        [M(O.AggressiveInlining)] public static bool EqualIgnoreCase( this string s1, string s2 ) => (string.Compare( s1, s2, true ) == 0);
        [M(O.AggressiveInlining)] public static bool ContainsIgnoreCase( this string s1, string s2 ) => ((s1 != null) && (s1.IndexOf( s2, StringComparison.InvariantCultureIgnoreCase ) != -1));

        /// <summary>
        /// Copy user settings from previous application version if necessary
        /// </summary>
        [M(O.AggressiveInlining)] public static void UpgradeIfNeed( this Settings settings )
        {
            // Copy user settings from previous application version if necessary
            if ( !settings._IsUpgradedInThisVersion )
            {
                settings.Upgrade();
                settings._IsUpgradedInThisVersion = true;
                settings.SaveNoThrow();
            }
        }
        [M(O.AggressiveInlining)] public static void SaveNoThrow( this Settings settings )
        {
            try
            {
                settings.Save();
            }
            catch ( Exception ex )
            {
                Debug.WriteLine( ex );
            }
        }

        public static void DeleteFiles_NoThrow( string[] fileNames )
        {
            if ( fileNames.AnyEx() )
            {
                foreach ( var fileName in fileNames )
                {
                    DeleteFile_NoThrow( fileName );
                }
            }
        }
        public static void DeleteFile_NoThrow( string fileName )
        {
            try
            {
                File.Delete( fileName );
            }
            catch ( Exception ex )
            {
                Debug.WriteLine( ex );
            }
        }

        public static void MessageBox_ShowInformation( this IWin32Window owner, string text, string caption ) => MessageBox.Show( owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information );
        public static void MessageBox_ShowError( this IWin32Window owner, string text, string caption ) => MessageBox.Show( owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
        public static void MessageBox_ShowError( this Exception ex, string caption ) => MessageBox_ShowError( ex.ToString(), caption );
        public static void MessageBox_ShowError( string text, string caption )
        {
            var form = Application.OpenForms.Cast< Form >().FirstOrDefault();
            if ( (form != null) && !form.IsDisposed && form.IsHandleCreated )
            {
                form.MessageBox_ShowError( text, caption );
            }
            else
            {
                MessageBox.Show( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
            }            
        }
        public static DialogResult MessageBox_ShowQuestion( this IWin32Window owner, string text, string caption
            , MessageBoxButtons buttons = MessageBoxButtons.YesNo, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1 )
            => MessageBox.Show( owner, text, caption, buttons, MessageBoxIcon.Question, defaultButton );

        public static void SetEnabledAllChildControls( this Control parentControl, bool enabled ) => parentControl.Controls.Cast< Control >().ToList().ForEach( c => c.Enabled = enabled );

        [M(O.AggressiveInlining)] public static string TrimIfLongest( this string s, int maxLength ) => ((maxLength < s.Length) ? (s.Substring( 0, maxLength ) + "..." ) : s);

        public static string ToJSON< T >( this T t )
        {
            var ser = new DataContractJsonSerializer( typeof(T) );

            using ( var ms = new MemoryStream() )
            {                
                ser.WriteObject( ms, t );
                var json = Encoding.UTF8.GetString( ms.GetBuffer(), 0, (int) ms.Position );
                return (json);
            }
        }
        public static T FromJSON< T >( string json )
        {
            var ser = new DataContractJsonSerializer( typeof(T) );

            using ( var ms = new MemoryStream( Encoding.UTF8.GetBytes( json ) ) )
            {
                var t = (T) ser.ReadObject( ms );
                return (t);
            }
        }

        public static void SetDoubleBuffered< T >( this T t, bool doubleBuffered ) where T : Control
        {          
            //Control.DoubleBuffered = true;
            var field = typeof(T).GetProperty( "DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance );
            field?.SetValue( t, doubleBuffered );
        }

        public static void SetValue( this NumericUpDown nud, int value ) => nud.Value = Math.Max( nud.Minimum, Math.Min( nud.Maximum, value ) );
        public static TimeSpan StopAndElapsed( this Stopwatch sw )
        {
            sw.Stop();
            return (sw.Elapsed);
        }
    }
}
