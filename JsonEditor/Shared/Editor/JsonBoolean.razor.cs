using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace JsonEditor.Shared.Editor
{
    public class JsonBooleanBase : ComponentBase
    {
        [Parameter, EditorRequired]
        public bool Value { get; set; } = false;

        [Parameter, EditorRequired]
        public Action<JToken> OnChange { get; set; } = default!;
    }
}
