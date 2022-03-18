using JsonEditor.Code;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace JsonEditor.Pages
{
    public class IndexBase : ComponentBase
    {
		[Inject]
		protected IWebHostEnvironment Environment { get; set; } = default!;

		private string basePath => Path.Combine(
			Environment.ContentRootPath,
			"schemas"
		);
		protected List<Schema> schemas = new();
		protected Schema? selectedSchema;
		protected string schemaSearch = string.Empty;
		protected bool showSchemaSelectDialog = false;
		private string json = string.Empty;
		protected string jsonContent { get => json; set
            {
				json = value;
				try
				{
					jsonObj = JToken.Parse(json);
				} catch (Exception)
                {
					jsonObj = null;
                }
			}
		}
		protected JToken? jsonObj;

		protected override void OnInitialized()
		{
			schemas = Schema.GetSchemas(Environment.ContentRootPath);
		}

		protected void OnSchemaSelected(Schema s)
		{
			selectedSchema = s;
			StateHasChanged();
			showSchemaSelectDialog = false;
		}

		protected void OnJsonChange(JToken jToken)
        {
			jsonContent = jToken.ToString();
			StateHasChanged();
        }
	}
}
