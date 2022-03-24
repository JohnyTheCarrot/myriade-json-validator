using JsonEditor.Shared.Generic;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace JsonEditor.Shared.SyntaxHighlighting;

public class JsonArrayBase : HoverableBase
{
    [Parameter, EditorRequired] public JArray JsonArray { get; set; } = default!;
}