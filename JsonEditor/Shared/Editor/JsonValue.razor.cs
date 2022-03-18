using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JsonEditor.Shared.Editor
{
    public class JsonValueBase : ComponentBase
    {
        [Parameter, EditorRequired]
        public JToken JsonValue { get; set; } = default!;

        [Parameter]
        public Action<JToken>? OnChange { get; set; }

        [Parameter, EditorRequired]
        public IList<ValidationError>? Errors { get; set; } = default!;

        [Parameter, EditorRequired]
        public JToken Root { get; set; } = default!;

        protected IEnumerable<ValidationError>? RelevantErrors => Errors?
            .Where(e => Root.SelectToken(e.Path) == JsonValue);

        [Parameter]
        public string? Name { get; set; }

        protected void OnChangeValue(JToken newValue)
        {
            JsonValue = newValue;
            OnChange?.Invoke(newValue);
        }

        protected void OnChangeString(string newValue)
        {
            var preparedString = JsonConvert.ToString(newValue);
            OnChangeValue(JToken.Parse(preparedString));
        }
    }
}
