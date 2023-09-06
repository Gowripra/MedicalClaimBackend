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

        [OneTimeSetUp]
        public void SetUp()
        {
            context = new DataContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDatabase();
            claimsController = new ClaimProcessRegistrationsController((IClaimRegService)context);
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
        public async Task NewClaimReg()
        {
            ClaimProcessRegistrationsController claims = new ClaimProcessRegistrationsController((IClaimRegService)context);
            var result = await claims.Add(new ClaimProcessRegistrationDTO
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
