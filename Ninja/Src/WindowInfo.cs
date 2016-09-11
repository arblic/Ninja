
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Ninja {

	/// <summary>
	/// ウィンドウ情報
	/// </summary>
	public class cWindowInfo {

		/// <summary>
		/// ctor
		/// </summary>
		public cWindowInfo( IntPtr _hWnd ) {
			hWnd		= _hWnd;
			Style		= Win32API.GetWindowLong( hWnd, GWL_STYLE );
			ClassName	= GetWindowClassName( hWnd );
			Title		= GetWindowTitle( hWnd );
			m_Children	= new List<cWindowInfo>();
			EnumChildren( this );
		}

		/// <summary>
		/// ウィンドウハンドルからウィンドウタイトル名朱徳
		/// </summary>
		private string GetWindowTitle( IntPtr hWnd ) {

			try {
				int textLen = Win32API.GetWindowTextLength( hWnd );
				if( 0 < textLen ) {
					StringBuilder StrBuf = new StringBuilder( textLen + 1 );
					Win32API.GetWindowText( hWnd, StrBuf, StrBuf.Capacity );
					return StrBuf.ToString();
				}
			} catch( Exception Exp ) {
				Debug.WriteLine( Exp.ToString() );
			}

			return "";
		}

		/// <summary>
		/// ウィンドウハンドルからクラス名取得
		/// </summary>
		private string GetWindowClassName( IntPtr hWnd ) {

			try {
				StringBuilder StrBuf = new StringBuilder( 256 );
				Win32API.GetClassName( hWnd, StrBuf, StrBuf.Capacity );
				return StrBuf.ToString();
			} catch( Exception Exp ) {
				Debug.WriteLine( Exp.ToString() );
			}

			return "";
		}

		/// <summary>
		/// 子ウィンドウの列挙
		/// </summary>
		private void EnumChildren( cWindowInfo WndInfo ) {
			IntPtr hChildAfter	= IntPtr.Zero;
			do {
				hChildAfter	= Win32API.FindWindowEx( WndInfo.hWnd, hChildAfter, null, null );
				if( IntPtr.Zero != hChildAfter ) {
					m_Children.Add( new cWindowInfo( hChildAfter ) );
				} else {
					break;
				}
			} while( IntPtr.Zero != hChildAfter );
		}

		/// <summary>
		/// ウィンドウハンドル
		/// </summary>
		public IntPtr	hWnd { get; private set; }

		/// <summary>
		/// スタイル
		/// </summary>
		public int		Style { get; private set; }

		/// <summary>
		/// クラス名
		/// </summary>
		public string	ClassName { get; private set; }

		/// <summary>
		/// タイトルバー
		/// </summary>
		public string	Title { get; private set; }

		/// <summary>
		/// 子供たち
		/// </summary>
		public IEnumerable<cWindowInfo> Children { get { return m_Children; } }

		/// <summary>
		/// 子供たち
		/// </summary>
		private List<cWindowInfo> m_Children;

		/// <summary>
		/// スタイル取得用
		/// </summary>
		private static int GWL_STYLE	= -16;
	}
}
