using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace JsonEditor.Shared.Editor
{
    public class JsonNumberBase : ComponentBase
    {
        [Parameter, EditorRequired]
        public float Value { get; set; } = default!;

        [Parameter]
        public Action<JToken>? OnChange { get; set; }

        public void OnInput(ChangeEventArgs e)
        {
            OnChange?.Invoke(float.Parse((e.Value as string)!));
        }
    }
}
