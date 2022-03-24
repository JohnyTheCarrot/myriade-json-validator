using JsonEditor.Shared.Generic;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace JsonEditor.Shared.SyntaxHighlighting;

public class JsonObjectBase : HoverableBase
{
    [Parameter, EditorRequired] public JObject JsonObject { get; set; } = default!;
}