using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Editor
{
    public class JsonStringBase : ComponentBase
    {
        [Parameter, EditorRequired]
        public string Value { get; set; } = default!;

        [Parameter]
        public Action<string>? OnChange { get; set; }

        public void OnInput(ChangeEventArgs e)
        {
            OnChange?.Invoke((e.Value as string)!);
        }
    }
}
