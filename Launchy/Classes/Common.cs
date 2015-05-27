using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Launchy.Classes
{
	public class Common
	{

		#region Singleton

		private static Common instance;
		public static Common Instance
		{
			get { return instance ?? (instance = new Common()); }
		}

		#endregion

		#region Constructor

		private Common()
		{
			// Empty constructor
		}

		#endregion

		#region Public methods

		public void NavigateToUrl(string url)
		{
			var uri = new Uri(url, UriKind.Relative);
			((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(uri);
		}

		#endregion

	}
}