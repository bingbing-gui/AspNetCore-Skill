using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.ModelBinding.Controllers;

[ApiController]
[Route("[controller]")]

public class PetsController : ControllerBase
{
    [HttpGet("{id}")]
    public ActionResult<Pet> GetById(int id, bool dogsOnly)
    {
        return NoContent();
    }
    [HttpPost("Create")]
    public ActionResult<Pet> Create([FromBody] Pet pet)
    {
        return NoContent();
    }
    [HttpPost("CreateList")]
    public ActionResult<Pet> CreateList([FromBody] List<Pet> pets)
    {
        return NoContent();
    }
    [HttpPost("Edit")]
    public ActionResult<Instructor> Edit([FromBody] Instructor instructor)
    {
        return NoContent();
    }
    
}
