using Bcc_Coding_challenge.Models;
using Google.Cloud.Diagnostics.Common;
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
            _configuration = configuration;
        }
        public class TraceSamplesConstructorController : Controller
        {
            private readonly IManagedTracer _tracer;

            /// <summary>
            /// The <see cref="IManagedTracer"/> is populated by dependency injection.
            /// </summary>
            public TraceSamplesConstructorController(IManagedTracer tracer)
            {
                _tracer = tracer;
            }

            public void TraceHelloWorld(string id)
            {
                // Change the name of the span to what makese sense in your context.
                using (_tracer.StartSpan(id))
                {
                    // The code whose execution is to be included in the span goes here.
                    ViewData["Message"] = "Hello World.";
                }
            }
        }

        /*[HttpGet(Name="random")]
        public int[] Generate()
        {
            return Randoms.nums();
        }
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

            Console.WriteLine("kiedy to pisze to jest 15:08");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(Environment.GetEnvironmentVariable("WHOLE_LINK")))
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
