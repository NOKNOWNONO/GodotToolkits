using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InfoSet;

public sealed record InfoConfig
{
	[JsonPropertyName("name")]
	public string Name { get; set; } = "";

	[JsonPropertyName("output_path")]
	public string OutputPath { get; set; } = "";

	[JsonPropertyName("namespace")]
	public string? Namespace { get; set; }

	[JsonPropertyName("properties")]
	public List<PropertyInfo> Properties { get; set; } = [];

	[JsonPropertyName("targets")]
	public Dictionary<string, TargetInfo> Targets { get; set; } = [];
}

public sealed record PropertyInfo
{
	[JsonPropertyName("name")]
	public string Name { get; set; } = "";

	[JsonPropertyName("type")]
	public string Type { get; set; } = "";

	[JsonPropertyName("value")]
	public string Value { get; set; } = "";

	[JsonPropertyName("options")]
	public PropertyOption? Options { get; set; }
}

public sealed record PropertyOption
{
	[JsonPropertyName("target")]
	public string Target { get; set; } = "";
}

public sealed record TargetInfo
{
	[JsonPropertyName("type")]
	public string Type { get; set; } = "";

	[JsonPropertyName("path")]
	public List<string> Path { get; set; } = [];

	[JsonPropertyName("file_path")]
	public string FilePath { get; set; } = "";
}
