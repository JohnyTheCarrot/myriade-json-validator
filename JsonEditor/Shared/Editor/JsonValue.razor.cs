using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonEditor.Shared.Editor
{
    public class JsonValueBase : ComponentBase
    {
        [Parameter, EditorRequired]
        public JToken JsonValue { get; set; } = default!;

        [Parameter]
        public Action<JToken>? OnChange { get; set; }

        [Parameter]
        public string? Name { get; set; }

        protected void OnChangeValue<T>(T newValue) where T : JToken
        {
            JsonValue = newValue;
            OnChange?.Invoke(newValue);
        }

        protected void OnChangeString(string newValue)
        {
            var preparedString = JsonConvert.ToString(newValue);
            OnChangeValue(JToken.Parse($"{preparedString}"));
        }
    }
}
