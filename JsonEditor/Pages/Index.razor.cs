using System;
using System.Collections.Generic;
using JsonEditor.Code;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JsonEditor.Pages
{
    public class IndexBase : ComponentBase
    {
		[Inject]
		protected IWebHostEnvironment Environment { get; set; } = default!;

		protected List<Schema> Schemas = new();
		protected Schema? SelectedSchema;
		protected bool ShowSchemaSelectDialog;
		private string _json = string.Empty;
		protected string JsonContent { get => _json; set
            {
				_json = value;
				try
				{
					JsonObj = JToken.Parse(_json);
				} catch (Exception)
                {
					JsonObj = null;
					return;
                }

				if (SelectedSchema == null) return;
				var schema = SelectedSchema.GetJSchema();
				JsonValid = JsonObj.IsValid(schema, out Errors);
            }
		}
		protected JToken? JsonObj;
		protected bool JsonValid;
		protected IList<ValidationError> Errors = new List<ValidationError>();

		protected override void OnInitialized()
		{
			Schemas = Schema.GetSchemas(Environment.ContentRootPath);
		}

		protected void OnSchemaSelected(Schema s)
		{
			SelectedSchema = s;
			StateHasChanged();
			ShowSchemaSelectDialog = false;
		}

		protected void OnJsonChange(JToken jToken)
        {
			JsonContent = jToken.ToString();
			StateHasChanged();
        }
	}
}
