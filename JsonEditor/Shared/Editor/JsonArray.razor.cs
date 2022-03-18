using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JsonEditor.Shared.Editor
{
    public class JsonArrayBase : ComponentBase
    {
        [Parameter, EditorRequired]
        public JArray JsonArray { get; set; } = default!;

        [Parameter]
        public Action<JArray>? OnChange { get; set; }

        [Parameter, EditorRequired]
        public IList<ValidationError>? Errors { get; set; } = default!;

        public void OnChangeProperty(int index, JToken newValue)
        {
            JsonArray[index] = newValue;
            OnChange?.Invoke(JsonArray);
        }
    }
}
