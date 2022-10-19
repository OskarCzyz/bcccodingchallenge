using Bcc_Coding_challenge.Models;
using Google.Cloud.Diagnostics.AspNetCore;
using Google.Cloud.Diagnostics.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Bcc_Coding_challenge.Controllers


{


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

    [Route("random")]

    public class RandomController : Controller
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Replace ProjectId with your Google Cloud Project ID.
            services.AddGoogleTraceForAspNetCore(new AspNetCoreTraceOptions
            {
                ServiceOptions = new Google.Cloud.Diagnostics.Common.TraceServiceOptions
                {
                    ProjectId = "Bcc_Coding_challenge"
                }
            });

            // Add any other services your application requires, for instance,
            // depending on the version of ASP.NET Core you are using, you may
            // need one of the following:

            // services.AddMvc();

            // services.AddControllersWithViews();
        }

        private readonly IConfiguration _configuration;

        public RandomController(IConfiguration configuration)
        {
            _configuration = configuration;
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
