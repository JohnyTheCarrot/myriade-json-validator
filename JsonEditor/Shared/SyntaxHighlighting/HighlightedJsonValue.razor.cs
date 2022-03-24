using JsonEditor.Shared.Generic;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace JsonEditor.Shared.SyntaxHighlighting;

public class HighlightedJsonValueBase : HoverableBase
{
    [Parameter, EditorRequired] public JToken JsonValue { get; set; } = default!;

    private string? _valuePath;
    
    protected bool Hover => InteractionStore.HoveredPath == _valuePath;

    protected override void OnInitialized()
    {
        _valuePath = JsonValue.Path;
    }

    protected void MouseOver()
    {
        if (Hover)
            return;

        InteractionStore.Update?.Invoke(InteractionStore.ClickedPath, JsonValue.Path);
    }

    protected void MouseClick()
    {
        InteractionStore.Update?.Invoke(JsonValue.Path, InteractionStore.HoveredPath);
    }
}