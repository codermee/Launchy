using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using ResourceLibrary;
using Launchy.Classes;
using System;
using System.Collections.ObjectModel;
using Microsoft.Phone.Shell;
using System.Linq;

namespace Launchy
{
	public partial class MainPage : PhoneApplicationPage
	{

		#region Members

		private ConnectionType SelectedItem { get; set; }
		private const string AboutPageUri = "/View/AboutPage.xaml";

		#endregion

		#region Constructor

		public MainPage()
		{
			InitializeComponent();
		}

		#endregion

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			var type = GetQuerystring();

			if (!String.IsNullOrEmpty(type))
			{
				var id = Convert.ToInt32(type);
                ClearQuerystring();
				ShowConnectionSettingsType(id);
			}

			settingsListBox.DataContext = GetConnectionTypes();
			SetupApplicationBar();
		}

		#region Private methods

		private void SetupApplicationBar()
		{
			var aboutMenuItem = (ApplicationBarMenuItem)ApplicationBar.MenuItems[0];
			aboutMenuItem.Text = AppResources.About;
		}

        private void ClearQuerystring()
        {
            NavigationContext.QueryString.Clear();
        }

		private string GetQuerystring()
		{
			string querystring;
			NavigationContext.QueryString.TryGetValue("type", out querystring);
			return querystring;
		}

		private void ShowConnectionSettingsType(int id)
		{
			var task = new ConnectionSettingsTask();
			switch (id)
			{
				case 0:
					task.ConnectionSettingsType = ConnectionSettingsType.Cellular;
					break;
				case 1:
					task.ConnectionSettingsType = ConnectionSettingsType.WiFi;
					break;
				case 2:
					task.ConnectionSettingsType = ConnectionSettingsType.Bluetooth;
					break;
				case 3:
					task.ConnectionSettingsType = ConnectionSettingsType.AirplaneMode;
					break;
				default:
					// do nothing
					break;
			}
			task.Show();
		}

		private IList<ConnectionType> GetConnectionTypes()
		{
			var list = new List<ConnectionType>
			    {
			        new ConnectionType 
							{
								Name = AppResources.Cellular,
								Type = ResourceLibrary.Type.Cellular,
                                ImageUrl = "Images/cellular-62.png",
								Id = 0
							},
					new ConnectionType 
							{
								Name = AppResources.WiFi,
								Type = ResourceLibrary.Type.WiFi,
                                ImageUrl = "Images/wifi-62.png",
								Id = 1
							},
					new ConnectionType 
							{
								Name = AppResources.Bluetooth,
								Type = ResourceLibrary.Type.Bluetooth,
                                ImageUrl = "Images/bluetooth-62.png",
								Id = 2
							},
					new ConnectionType 
							{
								Name = AppResources.AirplaneMode,
								Type = ResourceLibrary.Type.AirplaneMode,
								ImageUrl = "Images/airplanemode-62.png",
								Id = 3 
							}
			    };
			return list;
		}

		#endregion

		#region Events

		private void OnAppBarAboutMenuItemClick(object sender, EventArgs e)
		{
			Common.Instance.NavigateToUrl(AboutPageUri);
		}

		private void OnContextMenuPinToStartClick(object sender, RoutedEventArgs e)
		{
			TileUtility.Instance.CreateTile(SelectedItem);
		}

		private void GestureListener_Hold(object sender, GestureEventArgs e)
		{
			// sender is the Grid in this example 
			var item = ((Grid)sender).DataContext;

			// item has the type of the model
			SelectedItem = item as ConnectionType;
		}

		private void OnSettingsListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// If selected index is not -1 (no selection)
			if (settingsListBox.SelectedIndex != -1)
			{
				ShowConnectionSettingsType(settingsListBox.SelectedIndex);
			}
		}

		#endregion

	}
}