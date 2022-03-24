using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.SyntaxHighlighting;

public class JsonStringBase : ComponentBase
{
    [Parameter, EditorRequired] public string String { get; set; } = string.Empty;
    [Parameter] public bool IsKey { get; set; }
}