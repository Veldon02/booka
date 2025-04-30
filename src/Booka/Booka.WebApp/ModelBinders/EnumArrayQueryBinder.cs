namespace Booka.WebApp.ModelBinders;

public class EnumArrayQueryBinder<T> : ArrayQueryBinder<T>
{
    public override List<T> Parse(IEnumerable<string> values)
    {
        return values.Select(x => (T)Enum.Parse(typeof(T), x)).ToList();
    }
}