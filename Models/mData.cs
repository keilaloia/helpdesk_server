using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Data;
// using Newtonsoft.Json;
namespace helpdeskAPI.Models
{
    public class mData
    {
        public ulong id {get; set;}
        public string userName {get; set;}

        public string userPass {get; set;}

        public bool adminRole {get; set;}

        public bool helpDesk {get; set;}

        public bool userCred {get; set;}


    }
}