namespace RaylibSharp.Generator;

#pragma warning disable

using System.Text.Json.Serialization;
using System;

using System.Text.Json;
using System.Globalization;
using System.Runtime.InteropServices;

public partial class RaylibApi
{
    [JsonPropertyName("defines")]
    public Fields[] Defines { get; set; }

    [JsonPropertyName("structs")]
    public Struct[] Structs { get; set; }

    [JsonPropertyName("aliases")]
    public Fields[] Aliases { get; set; }

    [JsonPropertyName("enums")]
    public EnumDef[] Enums { get; set; }

    [JsonPropertyName("callbacks")]
    public Function[] Callbacks { get; set; }

    [JsonPropertyName("functions")]
    public Function[] Functions { get; set; }
}

public partial class Fields
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}

public partial class Function
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("returnType")]
    public string ReturnType { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("params")]
    public Param[] Params { get; set; }
}

public partial class Param
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public partial class EnumDef
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("values")]
    public ValueElement[] Values { get; set; }
}

public partial class ValueElement
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("value")]
    public long Value { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}

public partial class Struct
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("fields")]
    public Fields[] Fields { get; set; }
}

public partial class Welcome
{
    public static Welcome FromJson(string json)
    {
        return JsonSerializer.Deserialize<Welcome>(json, Converter.Settings);
    }
}

public static class Serialize
{
    public static string ToJson(this Welcome self)
    {
        return JsonSerializer.Serialize(self, Converter.Settings);
    }
}

internal static class Converter
{
    public static readonly JsonSerializerOptions Settings = new(JsonSerializerDefaults.General)
    {
        Converters =
            {
                new DateOnlyConverter(),
                new TimeOnlyConverter(),
                IsoDateTimeOffsetConverter.Singleton
            },
    };
}

public class DateOnlyConverter : JsonConverter<DateOnly>
{
    private readonly string serializationFormat;
    public DateOnlyConverter() : this(null) { }

    public DateOnlyConverter(string? serializationFormat)
    {
        this.serializationFormat = serializationFormat ?? "yyyy-MM-dd";
    }

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        return DateOnly.Parse(value!);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(serializationFormat));
    }
}

public class TimeOnlyConverter : JsonConverter<TimeOnly>
{
    private readonly string serializationFormat;

    public TimeOnlyConverter() : this(null) { }

    public TimeOnlyConverter(string? serializationFormat)
    {
        this.serializationFormat = serializationFormat ?? "HH:mm:ss.fff";
    }

    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        return TimeOnly.Parse(value!);
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(serializationFormat));
    }
}

internal class IsoDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    public override bool CanConvert(Type t)
    {
        return t == typeof(DateTimeOffset);
    }

    private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
    private string? _dateTimeFormat;
    private CultureInfo? _culture;

    public DateTimeStyles DateTimeStyles { get; set; } = DateTimeStyles.RoundtripKind;

    public string? DateTimeFormat
    {
        get => _dateTimeFormat ?? string.Empty;
        set => _dateTimeFormat = string.IsNullOrEmpty(value) ? null : value;
    }

    public CultureInfo Culture
    {
        get => _culture ?? CultureInfo.CurrentCulture;
        set => _culture = value;
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        string text;


        if ((DateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal
            || (DateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
        {
            value = value.ToUniversalTime();
        }

        text = value.ToString(_dateTimeFormat ?? DefaultDateTimeFormat, Culture);

        writer.WriteStringValue(text);
    }

    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? dateText = reader.GetString();

        if (string.IsNullOrEmpty(dateText) == false)
        {
            if (!string.IsNullOrEmpty(_dateTimeFormat))
            {
                return DateTimeOffset.ParseExact(dateText, _dateTimeFormat, Culture, DateTimeStyles);
            }
            else
            {
                return DateTimeOffset.Parse(dateText, Culture, DateTimeStyles);
            }
        }
        else
        {
            return default;
        }
    }


    public static readonly IsoDateTimeOffsetConverter Singleton = new();
}
#pragma warning restore
