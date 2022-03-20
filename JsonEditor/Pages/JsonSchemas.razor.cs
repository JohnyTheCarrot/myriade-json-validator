using System.Collections.Generic;
using System.Threading.Tasks;
using JsonEditor.Code;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;

namespace JsonEditor.Pages
{
    public class JsonSchemasBase : ComponentBase
    {
	    protected bool IsUploading, HasStartedUpload;

		[Inject]
		protected IWebHostEnvironment Environment { get; set; } = default!;

		protected readonly List<SchemaManagementResult> UploadResults = new();
		protected List<Schema> Schemas = new();

		protected void UpdateSchemasList()
		{
			Schemas = Schema.GetSchemas(Environment.ContentRootPath);
			StateHasChanged();
		}

		protected override void OnInitialized()
		{
			UpdateSchemasList();
		}

		protected async Task SaveSchema(InputFileChangeEventArgs e)
		{
			UploadResults.Clear();
			HasStartedUpload = true;
			if (e.FileCount > Schema.MaxNumberOfFilesPerUpload)
			{
				return;
			}
			IsUploading = true;
			var files = e.GetMultipleFiles(Schema.MaxNumberOfFilesPerUpload);

			foreach (var file in files)
			{
				var result = await Schema.SaveSchema(Environment.ContentRootPath, file);
				UploadResults.Add(result);
			}

			IsUploading = false;
			UpdateSchemasList();
		}
	}
}
