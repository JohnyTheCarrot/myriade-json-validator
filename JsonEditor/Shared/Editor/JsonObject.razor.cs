using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JsonEditor.Shared.Editor
{
    public class JsonObjectBase : ComponentBase
    {
        [Parameter, EditorRequired]
        public JObject JsonObject { get; set; } = default!;

        [Parameter]
        public Action<JObject>? OnChange { get; set; }

        [Parameter, EditorRequired]
        public IList<ValidationError>? Errors { get; set; } = default!;

        public void OnChangeProperty(string key, JToken newValue)
        {
            JsonObject[key] = newValue;
            OnChange?.Invoke(JsonObject);
        }
    }
}
