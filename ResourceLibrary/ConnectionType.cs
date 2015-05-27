namespace ResourceLibrary
{
	public enum Type
	{
		Cellular,
		WiFi,
		Bluetooth,
		AirplaneMode
	}

	public class ConnectionType
	{
		public string Name { get; set; }
		public Type Type { get; set; }
		public string ImageUrl { get; set; }
		public int Id { get; set; }
	}
}