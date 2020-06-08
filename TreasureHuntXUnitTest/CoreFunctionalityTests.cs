using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using TreasureHunt.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TreasureHunt.Data;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using TreasureHunt.Services;
using System.Security.Cryptography;
using System.Text;
using EO.WebBrowser.DOM;
using MathNet.Numerics;
using System.Net.Http;


//using Microsoft.Web.Mvc.Controls;

namespace TreasureHuntXUnitTest
{

    public class CoreFunctionalityTests
    {

        [Fact]
        public void Password_Hasher_Service_Test()
        {
            var demoPassword = "DemoPassword123";
            var hashResult = PasswordHasher.Hash(demoPassword);

            var localHashReturn = Hash(demoPassword);

            Assert.NotNull(hashResult);
            Assert.Matches(hashResult, localHashReturn);
        }

        private string Hash(string demoPassword)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(demoPassword));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        [Fact]
        public void Test_Score_Function()
        {

        }

        [Fact]
        public void Get_ScoreController_Test()
        {
            OnGet();

            Assert.Null(TopTenScores);
        }

        private TreasureHuntDbContext _database;
        public IList<Score> TopTenScores;

        public void LeaderboardModel(TreasureHuntDbContext database)
        {
            this._database = database;

            TopTenScores = new List<Score>();
        }

        public void OnGet()
        {
            // Get the top 10 scores, including the associated User. Ordered by the 
            TopTenScores = _database.Scores.Include(s => s.User).Take(10).OrderBy(s => s.TimeTakenSeconds).ToList();

        }

        //public static string Demo(int timeTakenSeconds) 
        //{
        //    fetch(window.location.protocol + '//' + window.location.host + '/Score/Add', {
        //    method: 'post',
        //    headers:
        //        {
        //            'Accept': 'application/json',
        //        'Content-Type': 'application/json'
        //    },
        //    body: JSON.stringify({ timeTakenSeconds: timeTakenSeconds })
        //     }).then(function(response) {
        //        return response.json();
        //    }).then(function(data) {
        //        console.log('Created score:', data.Id);
        //    });
        //}

        //private const string V = "2020-06-01";

        //[Fact]
        //public async Task ScoreController_GetAll()
        //{
        //    //TreasureHunt.Controllers.AccountController
        //    //arrange 
        //    var mockRepo = new Mock<ILogger>();
        //    mockRepo.Setup(repo => repo.AddAsync())
        //            .ReturnsAsync(GetTestSessions());
        //    var controller = new ScoreController(mockRepo.Object);
        //    //act
        //    var result = await controller.Index();
        //    //assert
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsAssignableFrom<IEnumerable<AddScoreRequest>>(viewResult.ViewData.Model);
        //    Assert.Equal(2, model.Count());
        //}
        //[Fact]
        //public void GetAllTest()
        //{
        //    var options = new DbContextOptionsBuilder<TreasureHuntDbContext>()
        //        .UseInMemoryDatabase(databaseName: "TreasureHuntDb")
        //        .Options;

        //    // Insert seed data into the database using one instance of the context
        //    using (var context = new TreasureHuntDbContext(options))
        //    {
        //        context.Scores.Add(new Score { Id = 7, TimeTakenSeconds = 30, ScoreDate = V, UserId = 3 });
        //        context.SaveChanges();
        //    }

        //    // Use a clean instance of the context to run the test
        //    using (var context = new TreasureHuntDbContext(options))
        //    {
        //        scoreRepository scoreRepository = new scoreRepository(context);
        //        List<Scores> scores == scoreRepository.GetAll();


        //    Assert.NotNull(context.Scores);
        //    }
        //}
    }
}


