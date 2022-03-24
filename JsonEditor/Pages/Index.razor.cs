using System.Text;
using JsonEditor.Code;
using JsonEditor.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JsonEditor.Pages;

public class IndexBase : ComponentBase
{
    [Inject] protected IWebHostEnvironment Environment { get; set; } = default!;

    protected List<Schema> Schemas = new();
    protected Schema? SelectedSchema;
    protected bool ShowSchemaSelectDialog;
    private string _json = string.Empty;

    private string JsonContent
    {
        set
        {
            _json = value;
            try
            {
                JsonObj = JToken.Parse(_json);

                if (SelectedSchema != null)
                    JsonObj.IsValid(SelectedSchema.GetJSchema(), out Errors);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            JsonObj = null;
        }
    }

    protected JToken? JsonObj;
    protected IList<ValidationError> Errors = new List<ValidationError>();
    protected JsonInteractionStore InteractionStore = new();

    private void UpdateInteractionStore(string? clickedPath, string? hoveredPath)
    {
        InteractionStore = new JsonInteractionStore
        {
            ClickedPath = clickedPath,
            HoveredPath = hoveredPath,
            Update = UpdateInteractionStore
        };
        StateHasChanged();
    }

    protected override void OnInitialized()
    {
        UpdateInteractionStore(null, null);
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

    protected async Task LoadFile(InputFileChangeEventArgs e)
    {
        var file = e.File;
        var jsonObj = new byte[file.Size];
        await file.OpenReadStream().ReadAsync(jsonObj);
        JsonContent = Encoding.UTF8.GetString(jsonObj)
            .Replace($"{(char) 0xFEFF}", string.Empty); // Notepad adds a 0xFEFF zero width space at the start of files, which breaks parsing.
        StateHasChanged();
    }
}