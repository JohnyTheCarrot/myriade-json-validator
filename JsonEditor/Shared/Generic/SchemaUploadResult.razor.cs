using JsonEditor.Code;
using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Generic
{
    public class SchemaUploadResultBase : ComponentBase
    {
        [Parameter, EditorRequired]
        public SchemaManagementResult? Result { get; set; }
    }
}
