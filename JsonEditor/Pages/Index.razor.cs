using JsonEditor.Code;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JsonEditor.Pages
{
    public class IndexBase : ComponentBase
    {
		[Inject]
		protected IWebHostEnvironment Environment { get; set; } = default!;

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
					return;
                }
				if (selectedSchema != null)
				{
					var schema = selectedSchema.GetJSchema();
					JsonValid = jsonObj.IsValid(schema, out errors);
				}
			}
		}
		protected JToken? jsonObj;
		protected bool JsonValid = false;
		protected IList<ValidationError> errors = new List<ValidationError>();

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
