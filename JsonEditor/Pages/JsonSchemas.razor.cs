using System.Text.Json;
using System.Text.RegularExpressions;
using System.Timers;
using JsonEditor.Code;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace JsonEditor.Pages
{
    public class JsonSchemasBase : ComponentBase
    {
		Regex fileNameRegex = new(@"^[\w\- \(\)\.]+\.json$"); // put common filename regex parts in separate constant => don't put in config, though
		Regex fileNameRenameRegex = new(@"^[\w\- \(\)\.]+$");
		protected bool isUploading = false, hasStartedUpload = false;

		[Inject]
		protected IWebHostEnvironment Environment { get; set; } = default!;

		protected string? errorMessage = null;
		protected List<SchemaManagementResult> uploadResults = new();
		protected List<Schema> schemas = new();
		public bool ShouldShowDeleteDialog = false;

		protected void UpdateSchemasList()
		{
			schemas = Schema.GetSchemas(Environment.ContentRootPath);
			StateHasChanged();
		}

		protected override void OnInitialized()
		{
			UpdateSchemasList();
		}

		protected async Task SaveSchema(InputFileChangeEventArgs e)
		{
			uploadResults.Clear();
			hasStartedUpload = true;
			if (e.FileCount > Schema.maxNumberOfFilesPerUpload)
			{
				errorMessage = $"Maximum number of files per upload exceeded: {Schema.maxNumberOfFilesPerUpload}";
				return;
			}
			isUploading = true;
			var files = e.GetMultipleFiles(Schema.maxNumberOfFilesPerUpload);

			foreach (var file in files)
			{
				var result = await Schema.SaveSchema(Environment.ContentRootPath, file);
				uploadResults.Add(result);
			}

			isUploading = false;
			UpdateSchemasList();
		}
	}
}
