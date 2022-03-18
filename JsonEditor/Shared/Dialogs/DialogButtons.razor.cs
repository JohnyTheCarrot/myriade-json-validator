using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Dialogs
{
	public partial class DialogButtons : ComponentBase
	{
		[Parameter]
		public RenderFragment? ChildContent { get; set; }
	}
}
