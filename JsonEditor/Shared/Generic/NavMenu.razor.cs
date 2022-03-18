using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Generic
{
    struct Link
    {
        public string Name;
        public string Path;
    }

    public partial class NavMenu : ComponentBase
    {
        private bool _shownMobile = false;
        private readonly Link[] links = {
            new Link { Name = "Configs", Path = "/" },
            new Link { Name = "Schema Manager", Path = "/schemas" },
        };
    }
}
