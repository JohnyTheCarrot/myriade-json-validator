using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Generic
{
    public struct Link
    {
        public string Name;
        public string Path;
    }

    public class NavMenuBase : ComponentBase
    {
        protected bool ShownMobile = false;
        protected readonly Link[] Links = {
            new() { Name = "Configs", Path = "/" },
            new() { Name = "Schema Manager", Path = "/schemas" },
        };
    }
}
