using BusinessLogicLayer.Services;
using DataAccesLayer.Models;
using Medical_Claim.Controllers;
using Medical_Claim.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_Claim.UnitTest.ControllerTest
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ClaimProcessRegistrationControllerTest
    {
        readonly Mock<ILogger<ClaimProcessRegistrationsController>> logger= new Mock<ILogger<ClaimProcessRegistrationsController>>();
        private static readonly DbContextOptions<DataContext> dbContextOptions = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "ClaimRegdb").Options;
        DataContext context;
        IClaimRegService _repo;
        ClaimProcessRegistrationsController claimsController;
        public ClaimProcessRegistrationControllerTest(IClaimRegService claimRegService)
        {
            _repo = claimRegService;
            claimsController = new ClaimProcessRegistrationsController(_repo);
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            context = new DataContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDatabase();
            claimsController = new ClaimProcessRegistrationsController(_repo);
        }
        [OneTimeTearDown]
        public void Cleanup()
        {
            context.Database.EnsureDeleted();
        }
        private void SeedDatabase()
        {
            var claim = new List<ClaimProcessRegistrationDTO>()
            {
                new ClaimProcessRegistrationDTO()
                {
                    ClaimRegID=1,
                    FullName="nvjfkdkd",
                    Email="add@gmail.com",
                    Password="nbhvf",

                },
                new ClaimProcessRegistrationDTO()
                {
                    ClaimRegID=2,
                    FullName="qowpwd",
                    Email="delete@gmail.com",
                    Password="cbaoap12"
                },
                new ClaimProcessRegistrationDTO()
                {
                    ClaimRegID=3,
                    FullName="uieoeppwd",
                    Email="update@gmail.com",
                    Password="abqooap12"
                }
            };
            context.claimProcessRegistrations.AddRange((IEnumerable<ClaimProcessRegistration>)claim);
            context.SaveChanges();
        }
        [Test]
        public void GetallTest()
        {
            var result = claimsController.GetallClaims();
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
        [Test]
        public async Task NewClaimReg()
        {
            //ClaimProcessRegistrationsController claims = new ClaimProcessRegistrationsController(_repo);
            var result = await claimsController.Add(new ClaimProcessRegistrationDTO
            {
                ClaimRegID=4,
                FullName="lsdmcksnjcks",
                Email="kskdkd@gmail.com",
                Password="jidf89"
            });
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
    }
}
