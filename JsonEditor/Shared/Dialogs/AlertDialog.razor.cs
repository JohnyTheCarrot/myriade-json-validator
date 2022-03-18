using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Dialogs
{
	public partial class AlertDialog : ComponentBase
	{
		[Parameter]
		public string Title { get; set; } = "Alert";

		[Parameter]
		public string ButtonConfirmText { get; set; } = "Okay";

		[Parameter]
		public RenderFragment? ChildContent { get; set; }

		[Parameter]
		public Action<bool>? OnShowChangeRequest { get; set; }

		[Parameter]
		public bool Show { get; set; }

		private void HideDialog()
		{
			Show = false;
		}
	}
}
