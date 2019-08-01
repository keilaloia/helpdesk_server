using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using helpdeskAPI.Models;
using Dapper;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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
        
        }

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<mData>>> GetDatamulti()
        // {
            
        //     var connection = _context.Database.GetDbConnection();
        //     var data = await connection.QueryAsync<mData>("SELECT * FROM loginUser");
        //     return data.ToList();
        //     // return await _context.mDatas.ToListAsync();
        // }

        // [HttpGet("{_id}")]
        // public async Task<ActionResult<IEnumerable<mData>>> GetDatabyID(ulong _id)
        // {
        //     var connection = _context.Database.GetDbConnection();

        //     var data = await connection.QueryAsync<mData>("SELECT * FROM user WHERE id = @myid;", new {myid = _id});
        //     return data.ToList();
        // }

        [HttpPost]//login user
        public async Task<ActionResult<IEnumerable<mData>>> PostData(mData data)
        {
               var connection = _context.Database.GetDbConnection();
            byte[] salt = new byte[128 / 8];
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password:  data.userPass,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8)
            );
            //check if user is in database
            var checkdb = await connection.QueryAsync<mData>("SELECT * FROM loginUser WHERE userName = @namecheck AND userPass = @passcheck",new {namecheck = data.userName, passcheck = hashed} );
                Console.WriteLine(checkdb);

            if(checkdb.Any())//checks if exists in db .any asks if elements are in inside checkdb
            {
                    var arr = checkdb.ToArray();
                    var checkRole = await connection.QueryAsync<mData>("SELECT * FROM userRole WHERE id = @_id",new {_id = arr[0].id} );
                return checkRole.ToList();

            }
            else
            {
                string sqlInsert = "INSERT INTO loginUser (userName, userPass) Values (@uName, @uPass );";
                sqlInsert += "INSERT INTO userRole (adminRole, helpDesk, userCred) Values (@aRole, @hDesk, @uCred);";
                sqlInsert += "INSERT INTO user(loginID, roleID)VALUEs(LAST_INSERT_ID(), LAST_INSERT_ID());";
                sqlInsert += "INSERT INTO history (userID) VALUES(LAST_INSERT_ID());";
    
                var affectedRows = await connection.ExecuteAsync(sqlInsert, new {uName = data.userName , uPass = hashed, aRole = data.adminRole, hDesk = data.helpDesk, uCred = data.userCred});
               
               var newUser = await connection.QueryAsync<mData>("SELECT * FROM user RIGHT JOIN loginUser ON user.loginID = loginUser.id RIGHT JOIN userRole ON user.roleID = userRole.id ORDER BY user.id DESC LIMIT 1" );


                return newUser.ToList();

            }

        }
        [HttpPost ("{ticket}")]
        public async
        Task<ActionResult<ticket>> PostTicket(ticket data)
        {
               var connection = _context.Database.GetDbConnection();
              string sqlInsert = "INSERT INTO ticket (content, TT, userID) Values (@_content,@TT, @currentuser);";
    
             var ticketinsert = await connection.ExecuteAsync(sqlInsert, new {_content = data.content, currentuser = data.id, TT =data.TT });
                return data;
        }
  
        [HttpGet ("ticket/{id}")]
        public async Task<ActionResult<IEnumerable<ticket>>> getTicket(ulong id)
        {
             var connection = _context.Database.GetDbConnection();
            var graball = await connection.QueryAsync<ticket>("SELECT * FROM ticket WHERE userID = @currentuser", new{currentuser = id});
            return graball.ToList();
        }

        //update database at id
        [HttpPut ("ticket/{id}")]
        public async Task<ActionResult<IEnumerable<ticket>>> PutTicket(ulong _id, ticket data)
        {
            var connection = _context.Database.GetDbConnection();
   
            string sqlUpdate = "UPDATE ticket SET content = @newcontent WHERE id = @myid;";
	        var affectedRows = await connection.QueryAsync<ticket>(sqlUpdate,  new {newcontent = data.content , myid = data.id});
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
