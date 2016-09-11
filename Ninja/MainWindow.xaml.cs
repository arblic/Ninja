using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Ninja {

	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window {

		/// <summary>
		/// ctor
		/// </summary>
		public MainWindow() {
			InitializeComponent();

			hWnd	= IntPtr.Zero;
		}

		/// <summary>
		/// ロードされた
		/// </summary>
		private void NinjaMainWindow_Loaded( object sender, RoutedEventArgs e ) {

			DispatcherTimer	Dt;

			try {

				Dt			= new DispatcherTimer( DispatcherPriority.Normal );
				Dt.Interval	= new TimeSpan( 0, 0, 1 );
				Dt.Tick		+= new EventHandler( Poling );

				Dt.Start();

			} catch( Exception Exp ) {
				Debug.WriteLine( Exp.ToString() );
			}
		}

		/// <summary>
		/// ポーリング
		/// </summary>
		private void Poling( object sender, EventArgs e ) {

			if( IntPtr.Zero == hWnd ) {

				Process[] Procs = Process.GetProcessesByName( "notepad" );

				if( 0 < Procs.Count() ) {

					cWindowInfo	wi	= new cWindowInfo( Procs[ 0 ].MainWindowHandle );

					hWnd	= wi.hWnd;

					ExecLog.AppendText( "メモ帳を見つけました\r\n" );
				}

			} else {

				if( !Win32API.IsWindow( hWnd ) ) {
					hWnd	= IntPtr.Zero;
					ExecLog.AppendText( "メモ帳を見失いました\r\n" );
				}
			}
		}

		/// <summary>
		/// 保持しているウィンドウハンドル
		/// </summary>
		IntPtr		hWnd;
	}
}
