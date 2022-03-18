using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace JsonEditor.Shared.Generic
{
	public enum ButtonVariant
	{
		Primary,
		Secondary,
		Danger
	}

	public partial class Button : ComponentBase
	{
		[Parameter]
		public ButtonVariant Variant { get; set; } = ButtonVariant.Primary;

		[Parameter]
		public RenderFragment? ChildContent { get; set; }

		[Parameter]
		public EventCallback<MouseEventArgs>? OnClick { get; set; } = null;

		[Parameter]
		public bool Disabled { get; set; }	= false;
	}
}
