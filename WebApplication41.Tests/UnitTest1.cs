using Microsoft.AspNetCore.Mvc;
using WebApplication41.Controllers;
using WebApplication41.Containers;

using Xunit;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
namespace WebApplication41.Tests
{
    public class UnitTest1
    {
        private readonly WeatherForecastController controller;
        public UnitTest1()
        {
            var mock = new Mock<Saver>();
            var checker = new Mock<Checker>();
            mock.Setup(repo => repo.GetAll("user.json")).ReturnsAsync((GetTestUsers()));
            mock.Setup(repo => repo.GetAll("user.json")).ReturnsAsync((GetTestUsers()));
            controller = new WeatherForecastController(checker.Object, mock.Object);
        }
        [Fact]
        public async void NewGameTest()
        {
           
            var okResult = await controller.NewGame() ;
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
            
        }
        
        private IDictionary<int, IEnumerable<string>> GetTestUsers()
        {
            var boards = new Dictionary<int, IEnumerable<string>>();
            boards[1] = new List<string>  
            { "*", "*", "*" ,
            "*", "*", "*"  ,
            "*", "*", "*"
            };
            boards[2] = new List<string>
            { "*", "*", "*" ,
            "*", "X", "*"  ,
            "*", "*", "*"
            };
            return boards;
        }
    }
}