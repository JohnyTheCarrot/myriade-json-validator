using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Schema;

namespace JsonEditor.Shared.Editor;

public class JsonStringBase : ComponentBase
{
    [Parameter, EditorRequired] public string Value { get; set; } = default!;

    [Parameter] public Action<string>? OnChange { get; set; }

    [Parameter] public JSchema? Schema { get; set; }

    protected bool IsEnum => Schema?.Enum.Count > 0;

    protected override void OnInitialized()
    {
        if (!IsEnum)
            return;

        var enumOption = Schema!.Enum[0];
        OnChange?.Invoke((string) enumOption!);
    }

    protected void OnInput(ChangeEventArgs e)
    {
        OnChange?.Invoke((e.Value as string)!);
    }

    protected void OnSelectChanged(string value)
    {
        OnChange?.Invoke(value);
    }
}