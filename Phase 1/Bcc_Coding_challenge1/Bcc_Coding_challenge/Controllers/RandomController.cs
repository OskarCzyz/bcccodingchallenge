using Bcc_Coding_challenge.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Bcc_Coding_challenge.Controllers

     
{

    [Route("random")]

    public class RandomController : Controller
    {

        private readonly IConfiguration _configuration;

        public RandomController(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        /*[HttpGet(Name="random")]
        public int[] Generate()
        {
            return Randoms.nums();
        }
           "ConnectionStrings": {
                "NumbersAppCon": "Server=/cloudsql/bcccodingchallenge:europe-central2:database-test;Database=test;Ssl Mode=Disable;User Id=postgres;Password=BazyDanych123"
            },
        */

    
        [HttpGet]
        public JsonResult Get()
        {
            int[] testing = Randoms.nums();
            string query = @"
                    select number
                    from numbers
                ";
                
            DataTable table = new DataTable();
            
            string NumbersAppCon1 = "Server=/cloudsql/bcccodingchallenge:europe-central2:database-test;Database=test;Ssl Mode=Disable;User Id=postgres;Password=BazyDanych123";

            string sqlDataSource = _configuration.GetConnectionString("NumbersAppCon1");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                for (int i = 0; i < testing.Length; i++)
                {
                    string query2 = $@"
                    insert into numbers(number)
                    values (@{testing[i]})
                ";
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query2, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        myCon.Close();
                    }
                }
                

                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
                
            }

            return new JsonResult(table);
        }

        
         

            
                
            
        

    }
}
