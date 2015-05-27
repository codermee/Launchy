namespace ResourceLibrary
{
	public class Localization
	{
		public Localization()
		{
		}

		private static AppResources localizedResources = new AppResources();
		public AppResources LocalizedResources
		{
			get { return localizedResources; }
		}
	}
}