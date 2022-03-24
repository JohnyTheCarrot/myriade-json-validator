using Microsoft.AspNetCore.Components;
using JsonEditor.Shared.Generic;

namespace JsonEditor.Shared.Dialogs;

public class ConfirmDialogBase : DialogBase
{
    [Parameter] public string ButtonConfirmText { get; set; } = "Confirm";

    [Parameter] public ButtonVariant ConfirmButtonVariant { get; set; } = ButtonVariant.Primary;

    [Parameter] public Action? OnCancelled { get; set; }

    [Parameter] public Action? OnConfirmed { get; set; }

    [Parameter] public bool ConfirmDisabled { get; set; }

    protected void Cancel()
    {
        Show = false;
        OnCancelled?.Invoke();
    }

    protected void Confirm()
    {
        Show = false;
        OnConfirmed?.Invoke();
    }
}