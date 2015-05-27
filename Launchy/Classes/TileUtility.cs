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
using System.Linq;
using Microsoft.Phone.Shell;
using ResourceLibrary;

namespace Launchy.Classes
{
	public class TileUtility
	{

		#region Singleton

		private static TileUtility instance;
		public static TileUtility Instance
		{
			get { return instance ?? (instance = new TileUtility()); }
		}

		#endregion

		#region Constructor

		private TileUtility()
		{
			// Empty constructor
		}

		#endregion

		public void RemoveTile(ConnectionType item)
		{
			var foundTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("type=" + item.Id));

			if (foundTile != null)
			{
				foundTile.Delete();
			}
		}

		public void CreateTile(ConnectionType item)
		{
			var foundTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("type=" + item.Id));

			var tile = SetupTileData(item);

			if (foundTile == null)
			{
				ShellTile.Create(new Uri(String.Format("/MainPage.xaml?type={0}", item.Id), UriKind.Relative), tile);
			}
			else
			{
				foundTile.Update(tile);
			}
		}

		#region Private methods

		private StandardTileData SetupTileData(ConnectionType item)
		{
			// TODO: Create BT image for tile
			Uri backgroundImage;

			switch (item.Type)
			{
				case ResourceLibrary.Type.AirplaneMode:
					backgroundImage = new Uri("/Images/airplanemode-173.png", UriKind.Relative);
					break;
				case ResourceLibrary.Type.Bluetooth:
                    backgroundImage = new Uri("Images/bluetooth-173.png", UriKind.Relative);
					break;
				case ResourceLibrary.Type.Cellular:
                    backgroundImage = new Uri("Images/cellular-173.png", UriKind.Relative);
					break;
				case ResourceLibrary.Type.WiFi:
                    backgroundImage = new Uri("Images/wifi-173.png", UriKind.Relative);
					break;
				default:
					backgroundImage = new Uri("ApplicationIcon.png");
					break;
			}

			var tile = new StandardTileData
								{
									BackgroundImage = backgroundImage, 
									Title = item.Name
								};
			return tile;
		}

		#endregion

	}
}