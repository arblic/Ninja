using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Ninja {

	/// <summary>
	/// Win32API 関数を使うためのクラス
	/// </summary>
	public class Win32API {

		public const int WM_LBUTTONDOWN = 0x201;
		public const int WM_LBUTTONUP = 0x202;
		public const int MK_LBUTTON = 0x0001;

		/// <summary>
		/// 指定したウィンドウハンドルにメッセージをお届け
		/// </summary>
		[DllImport("user32.dll")]
		public static extern int SendMessage( IntPtr hWnd, uint msg, uint wp, uint lp );

		/// <summary>
		/// ウィンドウを見つける
		/// </summary>
		[DllImport("user32.dll")]
		public static extern IntPtr FindWindowEx( IntPtr hWnd, IntPtr hwndChildAfter, string lpszClass, string lpszWindow );

		/// <summary>
		/// GWL_ 系のステータス朱徳
		/// </summary>
		[DllImport("user32")]
		public static extern int GetWindowLong( IntPtr hWnd, int nIndex );

		/// <summary>
		/// ウィンドウクラス名取得
		/// </summary>
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetClassName( IntPtr hWnd, StringBuilder lpClassName, int nMaxCount );

		/// <summary>
		/// ウィンドウテキスト長を朱徳
		/// </summary>
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowTextLength( IntPtr hWnd );

		/// <summary>
		/// ウィンドウテキストを朱徳
		/// </summary>
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowText( IntPtr hWnd, StringBuilder lpString, int nMaxCount );
	}
}
