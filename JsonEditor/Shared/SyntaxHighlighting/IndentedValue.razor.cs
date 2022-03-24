using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.SyntaxHighlighting;

public class IndentedValueBase : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}