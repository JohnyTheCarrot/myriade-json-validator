using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.SyntaxHighlighting;

public class JsonBooleanBase : ComponentBase
{
    [Parameter] public bool Value { get; set; }
}