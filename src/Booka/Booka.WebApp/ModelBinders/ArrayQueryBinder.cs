using Booka.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Booka.WebApp.ModelBinders;

public abstract class ArrayQueryBinder<T> : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var key = bindingContext.ModelName;
        var value = bindingContext.ValueProvider.GetValue(key);

        if (value.Length < 0)
        {
            return Task.CompletedTask;
        }

        var values = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries));

        try
        {
            bindingContext.Result = ModelBindingResult.Success(Parse(values));

            return Task.CompletedTask;
        }
        catch
        {
            throw new InvalidParametersException("Query binding failed");
        }
    }

    public abstract List<T> Parse(IEnumerable<string> values);
}