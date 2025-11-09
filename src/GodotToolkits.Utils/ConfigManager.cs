using System;
using System.Text.Json;

namespace GodotToolkits.Utils;

public class ConfigManager
{
	private JsonElement _config;

	private ConfigManager() { }

	public static bool TryParse(string json, out ConfigManager? manager)
	{
		try
		{
			var config = JsonSerializer.Deserialize<JsonElement>(json);
			manager = new ConfigManager { _config = config };
			return true;
		}
		catch (Exception)
		{
			manager = null;
			return false;
		}
	}

	public bool GenerateContentClass
	{
		get
		{
			try
			{
				return _config.GetProperty("GenerateContentClass").GetBoolean();
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
