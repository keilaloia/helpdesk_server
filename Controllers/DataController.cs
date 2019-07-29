using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using helpdeskAPI.Models;
using Dapper;
namespace helpdeskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly DataContext _context;
    //    string connStr = "server=localhost;port=3306;database=helpdesk_db;user=root;password=password";
        public DataController(DataContext context)
        {
            _context = context;
            // if(_context.mDatas.Count() == 0)
            // {
            //     _context.mDatas.Add(new mData {Name = "testname"});  
            //     _context.SaveChanges();
            // }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<mData>>> GetDatamulti()
        {
            
            var connection = _context.Database.GetDbConnection();
            var data = await connection.QueryAsync<mData>("SELECT * FROM user");
            return data.ToList();
            // return await _context.mDatas.ToListAsync();
        }

        [HttpGet("{_id}")]
        public async Task<ActionResult<IEnumerable<mData>>> GetDatabyID(ulong _id)
        {
            var connection = _context.Database.GetDbConnection();
            var data = await connection.QueryAsync<mData>("SELECT * FROM user WHERE id = @myid;", new {myid = _id});
            return data.ToList();
        }

        // add to database
        [HttpPost]
        public async Task<ActionResult<mData>> PostData(mData data)
        {
            var connection = _context.Database.GetDbConnection();
            string sqlInsert = "INSERT INTO user (userName, userPass) Values (@uName, @uPass );";
	        var affectedRows = await connection.ExecuteAsync(sqlInsert,  new {uName = data.userName , uPass = data.userPass});
            return data;
            // return CreatedAtAction(nameof(GetDatabyID), new {id = data.id}, data);
        }

        //update database at id
        [HttpPut ("{_id}")]
        public async Task<ActionResult<IEnumerable<mData>>> PutData(ulong _id, mData data)
        {
            var connection = _context.Database.GetDbConnection();
            string sqlUpdate = "UPDATE user SET userName = @uName, userPass = @uPass WHERE id = @myid;";
	        var affectedRows = await connection.QueryAsync<mData>(sqlUpdate,  new {uName = data.userName , uPass = data.userPass, myid = _id });
            return affectedRows.ToList();
        }

        //delete database item by id

        [HttpDelete("{_id}")]
        public async Task<ActionResult<IEnumerable<mData>>> DeleteDatabyID(ulong _id)
        {
           var connection = _context.Database.GetDbConnection();
            string sqlInsert = "DELETE FROM user WHERE id = @myid;";
	        var affectedRows = await connection.QueryAsync<mData>(sqlInsert,  new {myid = _id});
            return affectedRows.ToList();
        }


    }
}
