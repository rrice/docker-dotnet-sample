using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using MySimpleService.Test.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MySimpleService.Controllers
{
    public class WeatherForecastControllerTest
    {

        private Mock<ILogger<WeatherForecastController>> wcfLoggerMock;
        private HttpContext httpContext = new DefaultHttpContext();

        public WeatherForecastControllerTest()
        {
            wcfLoggerMock = new Mock<ILogger<WeatherForecastController>>();
        }

        [Fact]
        public async Task GetWeatherForecast_DefaultShouldReturnAll()
        {
            var service = new WeatherForecastController(wcfLoggerMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };
            var result = await service.Get();

            Assert.True(result.Count() > 1, "There should be at least one " +
            "default result.");
        }
    }
}
