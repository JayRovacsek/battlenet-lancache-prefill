﻿namespace BattleNetPrefill.Structs.Enums
{
    [JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default)]
    [JsonSerializable(typeof(List<Request>))]
    [JsonSerializable(typeof(ConcurrentDictionary<string, long>))]
    internal partial class SerializationContext : JsonSerializerContext
    {
    }
}