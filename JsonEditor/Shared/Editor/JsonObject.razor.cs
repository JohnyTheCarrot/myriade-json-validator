using System;
using System.Collections.Generic;
using JsonEditor.Code;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JsonEditor.Shared.Editor;

public class JsonObjectBase : ComponentBase
{
    [Parameter, EditorRequired]
    public JObject JsonObject { get; set; } = default!;

    [Parameter]
    public Action<JObject>? OnChange { get; set; }

    [Parameter, EditorRequired]
    public IList<ValidationError>? Errors { get; set; }
        
    [Parameter] public JSchema? Schema { get; set; }

    private string? _selectedProperty;

    protected void OnChangeProperty(string key, JToken newValue)
    {
        JsonObject[key] = newValue;
        OnChange?.Invoke(JsonObject);
    }

    protected JSchema? GetSchema(string key)
    {
        if (Schema == null)
            return null;

        Schema.Properties.TryGetValue(key, out var schema);
        return schema;
    }

    protected IEnumerable<string>? GetMissingProperties()
    {
        var properties = Schema?.Properties.Keys.Where(p => !JsonObject.ContainsKey(p)).ToList();

        if (
            properties?.Any() == true &&
            (
                _selectedProperty == null
                || _selectedProperty != null 
                && !properties.Contains(_selectedProperty)
            )
        )
            _selectedProperty = properties.First();

        return properties;
    }

    protected void SetSelectedProperty(string property)
    {
        _selectedProperty = property;
    }

    protected void AddProperty()
    {
        if (Schema == null || _selectedProperty == null)
            return;

        var schemaType = TypeUtils.JSchemaTypeToJTokenType((JSchemaType) Schema.Properties[_selectedProperty].Type!)[0];
        JsonObject[_selectedProperty] = TypeUtils.GetDefaultValue(schemaType);
        OnChange?.Invoke(JsonObject);
    }
}