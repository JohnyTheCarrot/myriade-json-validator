using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Dialogs
{
    public partial class Dialog : ComponentBase
    {
        [Parameter]
        public string Title { get; set; } = "No title provided";

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

		[Parameter]
		public Action<bool>? OnShowChangeRequest { get; set; }

		private bool _show = false;

		[Parameter]
		public bool Show
		{
			get => _show;
			set
			{
				_show = value;
				OnShowChangeRequest?.Invoke(_show);
			}
		}
	}
}
