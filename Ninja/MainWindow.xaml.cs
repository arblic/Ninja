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

			ExecLogScroll.ScrollToBottom();
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

				//Process[] Procs = Process.GetProcessesByName( "notepad" );
				Process[] Procs = Process.GetProcessesByName( "Calculator" );

				if( 0 < Procs.Count() ) {

					cWindowInfo	wi	= new cWindowInfo( Procs[ 0 ].MainWindowHandle );

					hWnd	= wi.hWnd;

					//Log( "メモ帳を見つけました" );

					TestOutput( "", wi );
				}

			} else {

				if( !Win32API.IsWindow( hWnd ) ) {
					hWnd	= IntPtr.Zero;
					//Log( "メモ帳を見失いました" );
				}
			}
		}

		private void TestOutput( string Space, cWindowInfo Wi ) {
			Log( Space + Wi.ClassName );
			foreach( cWindowInfo wi in Wi.Children ) {
				TestOutput( Space + "　", wi );
			}
		}

		/// <summary>
		/// ログ出力
		/// </summary>
		public void Log( string Text ) {

			DateTime	Dt	= DateTime.Now;

			ExecLog.AppendText( Dt.ToLocalTime() + ">" + Text + "\r\n" );

			ExecLogScroll.ScrollToBottom();
		}



		/// <summary>
		/// 保持しているウィンドウハンドル
		/// </summary>
		IntPtr		hWnd;
	}
}
