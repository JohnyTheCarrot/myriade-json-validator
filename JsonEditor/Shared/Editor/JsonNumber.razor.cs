﻿using System.Globalization;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace JsonEditor.Shared.Editor
{
    public class JsonNumberBase : ComponentBase
    {
        [Parameter, EditorRequired]
        public float Value { get; set; } = default!;

        [Parameter]
        public Action<JToken>? OnChange { get; set; }

        public void OnInput(ChangeEventArgs e)
        {
            var value = (e.Value as string)!;
            var safeValue = value.Length == 0 ? "0": value;
            if (!float.TryParse(safeValue, NumberStyles.Any, CultureInfo.InvariantCulture, out float valueFloat))
                return;

            if (valueFloat % 1f == 0f)
                OnChange?.Invoke((int) valueFloat);
            else
                OnChange?.Invoke(valueFloat);
        }
    }
}
