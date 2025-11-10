using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InfoGenerator;

public class InfoConfig
{
	[JsonPropertyName("name")]
	public string Name { get; set; } = "";

	[JsonPropertyName("output_path")]
	public string OutputPath { get; set; } = "";

	[JsonPropertyName("namespace")]
	public string NameSpace { get; set; } = "";

	[JsonPropertyName("push_file")]
	public string PushFile { get; set; } = "";

	[JsonPropertyName("targets")]
	public Dictionary<string, TargetInfo> Targets { get; set; } = [];

	[JsonPropertyName("properties")]
	public List<PropertyInfo> Properties { get; set; } = [];
}

public record TargetInfo
{
	[JsonPropertyName("type")]
	public string Type { get; set; } = "";

	[JsonPropertyName("file_path")]
	public string FilePath { get; set; } = "";

	[JsonPropertyName("path")]
	public List<string> Path { get; set; } = [];
}

public record PropertyInfo
{
	[JsonPropertyName("name")]
	public string Name { get; set; } = "";

	[JsonPropertyName("type")]
	public string Type { get; set; } = "";

	[JsonPropertyName("value")]
	public string Value { get; set; } = "";
}
