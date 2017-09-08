// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Reor
{
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string UserIdSettingsKey = "UserId";
		private static readonly string SettingsDefault = string.Empty;

		#endregion


		public static string UserId
		{
			get
			{
				return AppSettings.GetValueOrDefault(UserIdSettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(UserIdSettingsKey, value);
			}
		}

	}
}