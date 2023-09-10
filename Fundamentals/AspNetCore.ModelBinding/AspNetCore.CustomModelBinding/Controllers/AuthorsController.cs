using AspNetCore.CustomModelBinding.Data;
using Microsoft.AspNetCore.Mvc;


namespace AspNetCore.CustomModelBinding
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private readonly AuthorContext _context;

        public AuthorsController(AuthorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var author = _context.Authors?.Find(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }
    }
}
