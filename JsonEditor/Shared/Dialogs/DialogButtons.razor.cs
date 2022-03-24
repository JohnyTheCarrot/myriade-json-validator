using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Dialogs;

public class DialogButtonsBase : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}