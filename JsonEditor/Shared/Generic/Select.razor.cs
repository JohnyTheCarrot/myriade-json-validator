using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Generic;

public class SelectBase : ComponentBase
{
    [Parameter] public Action<string>? OnChange { get; set; }
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
}