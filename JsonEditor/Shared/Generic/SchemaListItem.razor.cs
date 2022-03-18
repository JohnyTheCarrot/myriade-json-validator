using JsonEditor.Code;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace JsonEditor.Shared.Generic
{
	public partial class SchemaListItem : ComponentBase
	{
		[Parameter]
		public Schema? Schema { get; set; }

		[Parameter]
		public Action? OnRefreshRequested { get; set; }

		private bool 
			ShowErrorDialog = false,
			ShowDeleteDialog = false,
			ShowRenameDialog = false;
		private string ErrorMessage = string.Empty;

		private string NewName = string.Empty;

		private bool ValidName => Schema.IsValidRenameFileName(NewName);

		private void PromptDelete()
		{
			ShowDeleteDialog = true;
		}

		private void Delete()
		{
			var result = Schema?.Delete();
			if (result is SchemaManagementFailure)
			{
				ShowErrorDialog = true;
				ErrorMessage = result.Message;
				return;
			}

			OnRefreshRequested?.Invoke();
		}

		private void PromptRename()
		{
			NewName = Path.GetFileNameWithoutExtension(Schema!.FileName);
			ShowRenameDialog = true;
		}

		private void Rename()
        {
			var result = Schema?.Rename(NewName);
			if (result is SchemaManagementFailure)
			{
				ShowErrorDialog = true;
				ErrorMessage = result.Message;
				ShowRenameDialog = false;
				StateHasChanged();
				return;
			}

			OnRefreshRequested?.Invoke();
		}

		private void OnShowChangeRequested(bool show)
		{
			ShowErrorDialog = show;
		}
	}
}
