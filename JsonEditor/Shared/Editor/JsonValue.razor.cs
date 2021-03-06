using JsonEditor.Code;
using JsonEditor.Shared.Generic;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JsonEditor.Shared.Editor;

public class JsonValueBase : HoverableBase
{
    [Parameter, EditorRequired] public JToken JsonValue { get; set; } = default!;

    [Parameter] public JSchema? Schema { get; set; }

    [Parameter] public Action<JToken>? OnChange { get; set; }

    [Parameter, EditorRequired] public IList<ValidationError>? Errors { get; set; }

    protected IEnumerable<ValidationError>? RelevantErrors => Errors?
        .Where(e => JsonValue.Root.SelectToken(e.Path) == JsonValue);

    [Parameter] public string? Name { get; set; }

    protected string? ValuePath;

    protected override void OnInitialized()
    {
        ValuePath = JsonValue.Path;
    }

    protected void OnChangeType(string stringValue)
    {
        if (!Enum.TryParse(stringValue, out JSchemaType value))
            return;
        var jTokenType = TypeUtils.JSchemaTypeToJTokenType(value);

        JsonValue = TypeUtils.GetDefaultValue(jTokenType[0]);
        OnChange?.Invoke(JsonValue);
    }

    protected void OnChangeValue(JToken newValue)
    {
        JsonValue = newValue;
        OnChange?.Invoke(newValue);
    }

    protected void OnChangeString(string newValue)
    {
        var preparedString = JsonConvert.ToString(newValue);
        OnChangeValue(JToken.Parse(preparedString));
    }
}