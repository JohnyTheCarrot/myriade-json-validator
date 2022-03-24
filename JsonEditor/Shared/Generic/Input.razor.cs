using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Generic;

public class InputBase<TValue> : ComponentBase
{
    [Parameter] public string Label { get; set; } = string.Empty;

    [Parameter] public string Type { get; set; } = "text";

    [Parameter] public Action<TValue?>? OnChange { get; set; }

    [Parameter] public EventCallback<ChangeEventArgs> OnInput { get; set; }

    [Parameter] public bool FocusOnShow { get; set; }

    [Parameter] public string ValidationErrorMessage { get; set; } = string.Empty;

    private TValue? _value;

    [Parameter]
    public TValue? Value
    {
        get => _value;
        set
        {
            _value = value;
            OnChange?.Invoke(_value);
        }
    }

    [Parameter] public string Placeholder { get; set; } = string.Empty;
}