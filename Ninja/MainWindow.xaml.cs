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
		public MainWindow() {

			InitializeComponent();

			DispatcherTimer	Dt;
			WatchDogs		wd	= new WatchDogs();

			Dt			= new DispatcherTimer( DispatcherPriority.Normal );
			Dt.Interval	= new TimeSpan( 0, 0, 1 );
			Dt.Tick		+= new EventHandler( Poling );
			Dt.Start();
		}

		/// <summary>
		/// ポーリング
		/// </summary>
		private void Poling( object sender, EventArgs e ) {

			Process[] Procs = Process.GetProcessesByName( "Calculator" );

			if( 0 < Procs.Count() ) {
				ExecLog.AppendText( "電卓を見つけました\r\n" );
			} else {
				ExecLog.AppendText( "電卓を見失いました\r\n" );
			}
		}
	}
}
