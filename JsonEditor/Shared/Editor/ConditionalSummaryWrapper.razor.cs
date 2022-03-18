using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Editor
{
    public class ConditionalSummaryWrapperBase : ComponentBase
    {
        [Parameter]
        public string? Summary { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
