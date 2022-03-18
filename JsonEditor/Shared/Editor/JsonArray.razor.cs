using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace JsonEditor.Shared.Editor
{
    public class JsonArrayBase : ComponentBase
    {
        [Parameter, EditorRequired]
        public JArray JsonArray { get; set; } = default!;

        [Parameter]
        public Action<JArray>? OnChange { get; set; }

        public void OnChangeProperty(int index, JToken newValue)
        {
            JsonArray[index] = newValue;
            OnChange?.Invoke(JsonArray);
        }
    }
}
