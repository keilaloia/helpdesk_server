using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Data;
// using Newtonsoft.Json;
namespace helpdeskAPI.Models
{
    public class mData
    {
        public ulong id {get; set;}
        public string userName {get; set;}//remove set

        public string userPass {get; set;}//remove set

        public bool IsComplete {get; set;}

        // public helpdesk_db db {get; set;}

        // public mData(helpdesk_db _db = null)
        // {
        //     this.db = _db;
        // }


        // public async Task InsertAsync()
        // {
        //     var cmd = db.Connection.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"INSERT INTO `user` (`userName`, `userPass`) VALUES (@Name, @Password;";
        //     BindParams(cmd);
        //     await cmd.ExecuteNonQueryAsync();
        //     Id = (ulong) cmd.LastInsertedId;
        // }


        // private void BindParams(MySqlCommand cmd)
        // {
        //     cmd.Parameters.Add(new MySqlParameter
        //     {
        //         ParameterName = "@userName",
        //         DbType = DbType.String,
        //         Value = Name
        //     });
        //     cmd.Parameters.Add(new MySqlParameter
        //     {
        //         ParameterName = "@userPass",
        //         DbType = DbType.String,
        //         Value = Password
        //     });
        // }
    }
}