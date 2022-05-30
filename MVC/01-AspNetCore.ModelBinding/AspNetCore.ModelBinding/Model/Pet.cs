using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.ModelBinding;

// <snippet_Class>
public class Pet
{
    public string Name { get; set; } = null!;

    [FromQuery] // Attribute is ignored.
    public string Breed { get; set; } = null!;
}
// </snippet_Class>
