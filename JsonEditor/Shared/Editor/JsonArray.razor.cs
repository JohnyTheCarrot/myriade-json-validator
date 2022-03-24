using JsonEditor.Code;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JsonEditor.Shared.Editor;

public class JsonArrayBase : ComponentBase
{
    [Parameter, EditorRequired] public JArray JsonArray { get; set; } = default!;

    [Parameter] public Action<JArray>? OnChange { get; set; }

    [Parameter, EditorRequired] public IList<ValidationError>? Errors { get; set; }

    [Parameter] public JSchema? Schema { get; set; }

    private string? _selectedType;
    protected IEnumerable<(string, JSchemaType)> PossibleTypes => TypeUtils.GetPossibleTypes(GetItemSchema(JsonArray.Count));

    protected void OnChangeProperty(int index, JToken newValue)
    {
        JsonArray[index] = newValue;
        OnChange?.Invoke(JsonArray);
    }

    protected JSchema? GetItemSchema(int index)
    {
        if (Schema == null)
            return null;

        return Schema.Items.Count == 1 ? Schema.Items[0] : Schema.Items[index];
    }

    protected void TypeSelected(string value)
    {
        _selectedType = value;
    }

    protected void Add()
    {
        _selectedType ??= PossibleTypes.First().Item2.ToString();

        var type = Enum.Parse<JSchemaType>(_selectedType);
        var jTokenType = TypeUtils.JSchemaTypeToJTokenType(type)[0];

        var defaultValue = TypeUtils.GetDefaultValue(jTokenType);
        JsonArray.Add(defaultValue);
        OnChange?.Invoke(JsonArray);
    }

    protected void RemoveItem(int index)
    {
        JsonArray.RemoveAt(index);
        OnChange?.Invoke(JsonArray);
    }
}