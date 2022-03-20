using System;
using Microsoft.AspNetCore.Components;
using JsonEditor.Shared.Generic;

namespace JsonEditor.Shared.Dialogs
{
	public partial class ConfirmDialog : ComponentBase
	{
		[Parameter]
		public string Title { get; set; } = "Alert";

		[Parameter]
		public string ButtonConfirmText { get; set; } = "Confirm";

		[Parameter]
		public ButtonVariant ConfirmButtonVariant { get; set; } = ButtonVariant.Primary;

		[Parameter]
		public RenderFragment? ChildContent { get; set; }

		[Parameter]
		public Action? OnCancelled { get; set; }

		[Parameter]
		public Action? OnConfirmed { get; set; }

		[Parameter]
		public Action<bool>? OnShowChangeRequest { get; set; }

		[Parameter]
		public bool Show { get; set; } = false;

		[Parameter]
		public bool ConfirmDisabled { get; set; } = false;

		private void Cancel()
		{
			Show = false;
			OnCancelled?.Invoke();
		}

		private void Confirm()
		{
			Show = false;
			OnConfirmed?.Invoke();
		}
	}
}
