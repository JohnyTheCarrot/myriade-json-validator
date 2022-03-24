using JsonEditor.Shared.Generic;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace JsonEditor.Shared.SyntaxHighlighting;

public class JsonHighlightingBase : HoverableBase
{
    [Parameter, EditorRequired] public JToken JsonValue { get; set; } = default!;
}