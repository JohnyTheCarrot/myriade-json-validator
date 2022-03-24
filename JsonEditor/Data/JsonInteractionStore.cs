namespace JsonEditor.Data;

public class JsonInteractionStore
{
    public string? ClickedPath { get; set; }
    public string? HoveredPath { get; set; }
    public Action<string?, string?>? Update;
}