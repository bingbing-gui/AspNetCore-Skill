using AspNetCore.CustomModelBinding.Binder;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.CustomModelBinding.Data
{
    [ModelBinder(BinderType = typeof(AuthorEntityBinder))]
    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? GitHub { get; set; }
        public string? Twitter { get; set; }
        public string? BlogUrl { get; set; }
    }
}
