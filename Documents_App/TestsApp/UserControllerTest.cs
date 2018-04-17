using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Controllers;
using WebApplication4.Entites;
using WebApplication4.Infrastructure;
using WebApplication4.Repository;
using WebApplication4.ViewModels;
using Xunit;

namespace TestsApp
{
    public class UserControllerTest
    {
        private string login = "postgres";
        private string password = "111";

        [Fact]
        public void IndexUsers()
        {
            Mock<IConfiguration> conf = new Mock<IConfiguration>();

            conf.Setup(c => c.GetSection("DBInfo:ConnectionString").Value).Returns("User ID=postgres;Password=111;Host=localhost;Port=5432;Database=Documents;Pooling=true;");

            var uCon = new UserRepository(conf.Object, login, password);

            var result = uCon.FindAll();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task InsertUsersAsync()
        {
            Mock<IConfiguration> conf = new Mock<IConfiguration>();

            conf.Setup(c => c.GetSection("DBInfo:ConnectionString").Value).Returns("User ID=postgres;Password=111;Host=localhost;Port=5432;Database=Documents;Pooling=true;");

            var uCon = new UserRepository(conf.Object, login, password);

            var us = new Users
            {
                UserName = "Buka",
                TableName = "Graph"
            };

            var result = uCon.Add(us);

            Assert.NotNull(result);
        }
    }
}
