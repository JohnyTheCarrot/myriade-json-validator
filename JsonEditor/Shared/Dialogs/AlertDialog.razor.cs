using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Dialogs;

public class AlertDialogBase : DialogBase
{
    [Parameter] public string ButtonConfirmText { get; set; } = "Okay";

    protected void HideDialog()
    {
        Show = false;
    }
}