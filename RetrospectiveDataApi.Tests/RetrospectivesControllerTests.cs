using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using RetrospectiveDataApi.Controllers;
using RetrospectiveDataApi.Exceptions;
using RetrospectiveDataApi.Models;
using RetrospectiveDataApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetrospectiveDataApi.Tests
{
    public class Tests
    {
        private Mock<IRetrospectiveDataRepository> _retrospectiveDataRepository;
        private Mock<IConfiguration> _configuration;
        private RetrospectivesController _retrospectivesController;

        #region private methods

        private void Setup()
        {
            _retrospectiveDataRepository = new Mock<IRetrospectiveDataRepository>();
            _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(c => c["FilePath"]).Returns("MockFileName");

            _retrospectivesController = new RetrospectivesController(_retrospectiveDataRepository.Object, _configuration.Object);
        }

        private void SetupValidMockData()
        {
            Setup();

            _retrospectiveDataRepository.Setup(x => x.Get(It.IsAny<string>()))
                                 .ReturnsAsync(new List<RetrospectiveFeedback>
                                        {
                                            new RetrospectiveFeedback{Name="Retro1", Date = DateTime.Now, Summary = "This is retro1"}
                                        });

        }

        private void SetupEmptyListOfRetrospectiveFeedbackData()
        {
            Setup();

            //Set to return empty list
            _retrospectiveDataRepository.Setup(x => x.Get(It.IsAny<string>()))
                                 .ReturnsAsync(new List<RetrospectiveFeedback>());

        }

        private void SetupDataToThrowException()
        {
            Setup();
            //Set to throw exception
            _retrospectiveDataRepository.Setup(x => x.Get(It.IsAny<string>()))
                                 .ThrowsAsync(new Exception("Error fetching data"));
        }

        private static object[] RetrospectivesControllerArguments =
        {
            new object[]{new Mock<IRetrospectiveDataRepository>(), null },
            new object[]{null, new Mock<IConfiguration>() }
        };
        #endregion


        [Test, TestCaseSource("RetrospectivesControllerArguments")]
        public void Test_When_Null_Arguments_In_RetrospectivesController_Then_Raise_ArgumentNullException(Mock<IRetrospectiveDataRepository> retrospectiveDataRepository, Mock<IConfiguration> configuration)
        {
            Assert.Throws<ArgumentNullException>(() => new RetrospectivesController
                    (
                        retrospectiveDataRepository != null ? retrospectiveDataRepository.Object : null,
                        configuration != null ? configuration.Object : null
                    ));
        }

        [Test]
        public void Test_When_Invalid_Config_Then_Raise_InvalidOperationException()
        {
            _configuration.SetupGet(c => c["FilePath"]).Returns(string.Empty);

            Assert.Throws<InvalidOperationException>(() => new RetrospectivesController(_retrospectiveDataRepository.Object, _configuration.Object));
        }

        [Test]
        public void Test_Get_When_SuccessResponse_Then_Returns_OK_Result()
        {
            //Arrange
            SetupValidMockData();

            //Act
            var result = _retrospectivesController.Get().Result;

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void Test_Get_When_EmptyRetrospectiveDataResponse_Then_Returns_NoContent()
        {
            //Arrange
            SetupEmptyListOfRetrospectiveFeedbackData();

            //Act
            var result = _retrospectivesController.Get().Result;

            //Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void Test_Get_When_InternalError_Returns_500_ErrorCodeResponse()
        {
            //Arrange
            SetupDataToThrowException();

            //Act
            var result = _retrospectivesController.Get().Result;

            //Assert
            Assert.AreEqual(StatusCodes.Status500InternalServerError, (result as ObjectResult).StatusCode);
        }

        public void Test_Post_When_InvalidInput_Then_Returns_BadRequest()
        {
            //Arrange
            Setup();

            //Act
            var result = _retrospectivesController.Post(null).Result;

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void Test_Post_When_ValidInput_Then_Returns_CreatedAtActionResult()
        {
            //Arrange
            Setup();
            var retro = new Retrospective { Name = "Retro1", Date = "01/12/2019", Summary = "This is retro1" };

            _retrospectiveDataRepository.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<RetrospectiveFeedback>()))
                                             .ReturnsAsync(new RetrospectiveFeedback());
            //Act
            var result = _retrospectivesController.Post(retro).Result;

            //Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public void Test_Post_When_InvalidData_Returns_BadRequest()
        {
            //Arrange
            Setup();
            var retro = new Retrospective();

            //Set to throw exception
            _retrospectiveDataRepository.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<RetrospectiveFeedback>()))
                                 .ThrowsAsync(new RetrospectiveDataException("Invalid post data"));

            //Act
            var result = _retrospectivesController.Post(retro).Result;

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);

        }

        [Test]
        public void Test_Post_When_InternalError_Returns_500_ErrorCodeResponse()
        {
            //Arrange
            Setup();
            var retro = new Retrospective { Name = "Retro1", Date = "01/12/2019", Summary = "This is retro1" };

            //Set to throw exception
            _retrospectiveDataRepository.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<RetrospectiveFeedback>()))
                                 .ThrowsAsync(new Exception("Internal server error"));

            //Act
            var result = _retrospectivesController.Post(retro).Result;

            //Assert
            Assert.AreEqual(StatusCodes.Status500InternalServerError, (result as ObjectResult).StatusCode);

        }

    }
}