using AspNetCore.CustomModelBinding.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetCore.CustomModelBinding.Binder
{
    public class AuthorEntityBinder : IModelBinder
    {
        private readonly AuthorContext _authorContext;
        public AuthorEntityBinder(AuthorContext authorContext)
        {
            _authorContext = authorContext;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            var modelName = bindingContext.ModelName;

            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }
            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }
            if (!int.TryParse(value, out var id))
            {
                // Non-integer arguments result in model state errors
                bindingContext.ModelState.TryAddModelError(
                    modelName, "Author Id must be an integer.");

                return Task.CompletedTask;
            }

            // Model will be null if not found, including for
            // out of range id values (0, -3, etc.)
            var model = _authorContext.Authors.Find(id);
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}
