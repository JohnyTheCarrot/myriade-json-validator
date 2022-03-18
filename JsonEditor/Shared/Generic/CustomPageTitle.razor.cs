using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Generic
{
	public partial class CustomPageTitle : ComponentBase
	{
		[Parameter]
		public string Title { get; set; } = "Title";
	}
}
