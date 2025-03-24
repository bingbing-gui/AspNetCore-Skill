﻿using AspNetCore.HttpClientWithHttpVerb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCore.HttpClientWithHttpVerb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;
        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TodoItem>> Get() =>
             await _context.TodoItems.AsNoTracking().ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> Get(long id)
        {
            var request = HttpContext.Request;
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return todoItem;
        }
        [HttpPost]
        public async Task<IActionResult> Post(TodoItem todoItem)
        {
            var request = HttpContext.Request;
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Get), new { Id = todoItem.Id, todoItem });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, TodoItem todoItem)
        {
            var request = HttpContext.Request;
            if (todoItem.Id != id)
                return BadRequest();
            _context.Update(todoItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var request = HttpContext.Request;
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
