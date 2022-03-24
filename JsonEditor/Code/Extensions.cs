namespace JsonEditor.Code;

public static class Extensions
{
    public static IEnumerable<(T item, int index)> Indexed<T>(this IEnumerable<T> self)
        => self.Select((item, index) => (item, index));
}