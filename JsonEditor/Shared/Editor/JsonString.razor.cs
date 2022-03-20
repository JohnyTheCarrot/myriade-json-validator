using System;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Schema;

namespace JsonEditor.Shared.Editor
{
    public class JsonStringBase : ComponentBase
    {
        [Parameter, EditorRequired]
        public string Value { get; set; } = default!;

        [Parameter]
        public Action<string>? OnChange { get; set; }
        
        [Parameter] public JSchema? Schema { get; set; }

        protected void OnInput(ChangeEventArgs e)
        {
            OnChange?.Invoke((e.Value as string)!);
        }
    }
}
