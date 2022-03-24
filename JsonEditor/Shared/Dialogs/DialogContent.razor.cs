using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Dialogs;

public class DialogContentBase : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}