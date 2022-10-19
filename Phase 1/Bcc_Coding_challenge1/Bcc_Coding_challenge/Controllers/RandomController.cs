using Bcc_Coding_challenge.Models;
using Google.Api;
using Google.Cloud.Diagnostics.AspNetCore;
using Google.Cloud.Diagnostics.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using static Google.Api.Gax.Grpc.Gcp.AffinityConfig.Types;

namespace Bcc_Coding_challenge.Controllers


{

    [Route("random")]

    public class RandomController : Controller
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Replace ProjectId with your Google Cloud Project ID.
            // Replace Service with a name or identifier for the service.
            // Replace Version with a version for the service.
            services.AddGoogleDiagnosticsForAspNetCore("bcccodingchallenge", "netcoreapi", "latest");
            services.AddGoogleErrorReportingForAspNetCore(new Google.Cloud.Diagnostics.Common.ErrorReportingServiceOptions
            {
                // Replace ProjectId with your Google Cloud Project ID.
                ProjectId = "bcccodingchallenge",
                // Replace Service with a name or identifier for the service.
                ServiceName = "netcoreapi",
                // Replace Version with a version for the service.
                Version = "latest"
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
