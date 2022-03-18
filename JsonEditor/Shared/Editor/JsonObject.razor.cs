using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace JsonEditor.Shared.Editor
{
    public class JsonObjectBase : ComponentBase
    {
        [Parameter, EditorRequired]
        public JObject JsonObject { get; set; } = default!;

        [Parameter]
        public Action<JObject>? OnChange { get; set; }

        public void OnChangeProperty(string key, JToken newValue)
        {
            JsonObject[key] = newValue;
            OnChange?.Invoke(JsonObject);
        }
    }
}
