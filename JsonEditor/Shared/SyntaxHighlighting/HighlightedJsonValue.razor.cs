using JsonEditor.Shared.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;

namespace JsonEditor.Shared.SyntaxHighlighting;

public class HighlightedJsonValueBase : HoverableBase
{
    [Parameter, EditorRequired] public JToken JsonValue { get; set; } = default!;

    protected string? ValuePath;

    protected override void OnInitialized()
    {
        ValuePath = JsonValue.Path;
    }
}