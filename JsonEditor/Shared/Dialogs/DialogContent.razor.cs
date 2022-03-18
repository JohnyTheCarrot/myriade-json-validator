using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Dialogs
{
	public partial class DialogContent : ComponentBase
	{
		[Parameter]
		public RenderFragment? ChildContent { get; set; }
	}
}
