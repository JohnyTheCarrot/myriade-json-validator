using System;
using System.IO;
using JsonEditor.Code;
using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Generic
{
	public class SchemaListItemBase : ComponentBase
	{
		[Parameter]
		public Schema? Schema { get; set; }

		[Parameter]
		public Action? OnRefreshRequested { get; set; }

		protected bool 
			ShowErrorDialog,
			ShowDeleteDialog,
			ShowRenameDialog;
		protected string ErrorMessage = string.Empty;

		protected string NewName = string.Empty;

		protected bool ValidName => Schema.IsValidRenameFileName(NewName);

		protected void PromptDelete()
		{
			ShowDeleteDialog = true;
		}

		protected void Delete()
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

		protected void PromptRename()
		{
			NewName = Path.GetFileNameWithoutExtension(Schema!.FileName);
			ShowRenameDialog = true;
		}

		protected void Rename()
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

		protected void OnShowChangeRequested(bool show)
		{
			ShowErrorDialog = show;
		}
	}
}
