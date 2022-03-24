using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.SyntaxHighlighting;

public class JsonNumberBase<TValue> : ComponentBase where TValue : struct
{
    [Parameter] public TValue Value { get; set; }
}