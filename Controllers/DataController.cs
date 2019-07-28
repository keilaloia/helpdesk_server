using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using helpdeskAPI.Models;

namespace helpdeskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly DataContext _context;

        public DataController(DataContext context)
        {
            _context = context;
            if(_context.mDatas.Count() == 0)
            {
                _context.mDatas.Add(new mData {Name = "testname"});  
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<mData>>> GetDatamulti()
        {
            return await _context.mDatas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<mData>> GetDatabyID(ulong id)
        {
            var result = await _context.mDatas.FindAsync(id);

            if(result == null)
            {
                return NotFound();
            }

            return result;
        }

        //add to database
        [HttpPost]
        public async Task<ActionResult<mData>> PostData(mData data)
        {
            _context.mDatas.Add(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDatabyID), new {id = data.Id}, data);
        }

        //update database at id
        [HttpPut ("{id}")]
        public async Task<IActionResult> PutData(ulong id, mData data)//recheck code to see why it times out
        {
            if(id != data.Id)
            {
                
                return BadRequest();
            }

            _context.Entry(data).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //delete database item by id

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDatabyID(ulong id)
        {
            var dResult = await _context.mDatas.FindAsync(id);

            if(dResult == null)
            {
                NotFound();
            }

            _context.mDatas.Remove(dResult);
            await _context.SaveChangesAsync();

            return NoContent();

        }


    }
}
