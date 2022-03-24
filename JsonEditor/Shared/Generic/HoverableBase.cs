using JsonEditor.Data;
using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Generic;

public class HoverableBase : ComponentBase
{
    [CascadingParameter] protected JsonInteractionStore InteractionStore { get; set; } = default!;
}