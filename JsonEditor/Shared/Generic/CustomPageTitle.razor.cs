using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Generic
{
	public class CustomPageTitleBase : ComponentBase
	{
		[Parameter]
		public string Title { get; set; } = "Title";
	}
}
