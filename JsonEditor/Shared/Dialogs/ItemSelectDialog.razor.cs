using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Dialogs
{
    public partial class ItemSelectDialog<TItem> : ComponentBase where TItem : class
    {
        [Parameter]
        public string Title { get; set; } = "Choose";

        [Parameter, EditorRequired]
        public RenderFragment<TItem>? RowTemplate { get; set; }

        [Parameter, EditorRequired]
        public Func<TItem, string>? SearchByExtractor { get; set; }

        [Parameter, EditorRequired]
        public IEnumerable<TItem>? Data { get; set; }

        [Parameter]
        public Action? OnCancelled { get; set; }

        [Parameter]
        public Action<TItem>? OnConfirmed { get; set; }

        [Parameter]
        public Action<bool>? OnShowChangeRequest { get; set; }

        [Parameter]
        public bool Show { get; set; } = false;

        private TItem? selectedItem;

        private string search = string.Empty;

        private IEnumerable<TItem> DataFiltered => Data!.Where(r => SearchByExtractor!.Invoke(r).ToLower().Contains(search.ToLower()));

        private char[] Letters
        {
            get => DataFiltered!
                    .Select(SearchByExtractor!)
                    .Select(s => s[0])
                    .Distinct()
                    .OrderBy(c => c)
                    .ToArray();
        }

        private void SearchUpdated(ChangeEventArgs e)
        {
            search = ((string?) e.Value)!;
        }
    }
}
