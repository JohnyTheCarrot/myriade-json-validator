using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace JsonEditor.Shared.Dialogs;

public class ItemSelectDialogBase<TItem> : DialogBase where TItem : class
{
    [Parameter, EditorRequired] public RenderFragment<TItem>? RowTemplate { get; set; }

    [Parameter, EditorRequired] public Func<TItem, string>? SearchByExtractor { get; set; }

    [Parameter, EditorRequired] public IEnumerable<TItem>? Data { get; set; }

    [Parameter] public Action? OnCancelled { get; set; }

    [Parameter] public Action<TItem>? OnConfirmed { get; set; }

    protected TItem? SelectedItem;

    protected string Search = string.Empty;

    protected IEnumerable<TItem> DataFiltered =>
        Data!.Where(r => SearchByExtractor!.Invoke(r).ToLower().Contains(Search.ToLower()));

    protected char[] Letters =>
        DataFiltered
            .Select(SearchByExtractor!)
            .Select(s => s[0])
            .Distinct()
            .OrderBy(c => c)
            .ToArray();

    protected void SearchUpdated(ChangeEventArgs e)
    {
        Search = ((string?) e.Value)!;
    }
}