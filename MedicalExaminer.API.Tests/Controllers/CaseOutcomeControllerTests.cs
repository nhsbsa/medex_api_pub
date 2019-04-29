﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MedicalExaminer.API.Controllers;
using MedicalExaminer.API.Models.v1.CaseOutcome;
using MedicalExaminer.Common.Loggers;
using MedicalExaminer.Common.Queries.CaseOutcome;
using MedicalExaminer.Common.Queries.Examination;
using MedicalExaminer.Common.Queries.User;
using MedicalExaminer.Common.Services;
using MedicalExaminer.Models;
using MedicalExaminer.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace MedicalExaminer.API.Tests.Controllers
{
    public class CaseOutcomeControllerTests : ControllerTestsBase<CaseOutcomeController>
    {
        [Fact]
        public void GetCaseOutcome_When_Called_With_Id_Not_Found_Returns_NotFound()
        {
            // Arrange
            var logger = new Mock<IMELogger>();
            var mapper = new Mock<IMapper>();
            var examinationId = "7E5D50CE-05BF-4A1F-AA6E-25418A723A7F";
            var examination = new Examination
            {
                ExaminationId = examinationId
            };

            var usersRetrievalByEmailService = new Mock<IAsyncQueryHandler<UserRetrievalByEmailQuery, MeUser>>();
            var mockMeUser = new Mock<MeUser>();
            usersRetrievalByEmailService.Setup(service => service.Handle(It.IsAny<UserRetrievalByEmailQuery>()))
                .Returns(Task.FromResult(mockMeUser.Object));

            var closeCaseService = new Mock<IAsyncQueryHandler<CloseCaseQuery, string>>();
            var coronerReferralService = new Mock<IAsyncQueryHandler<CoronerReferralQuery, string>>();

            var examinationRetrievalService = new Mock<IAsyncQueryHandler<ExaminationRetrievalQuery, Examination>>();
            examinationRetrievalService.Setup(service => service.Handle(It.IsAny<ExaminationRetrievalQuery>()))
                .Returns(Task.FromResult(examination));

            var saveOutstandingCaseItems = new Mock<IAsyncQueryHandler<SaveOutstandingCaseItemsQuery, string>>();

            var sut = new CaseOutcomeController(
                logger.Object,
                mapper.Object,
                coronerReferralService.Object,
                closeCaseService.Object,
                examinationRetrievalService.Object,
                saveOutstandingCaseItems.Object,
                usersRetrievalByEmailService.Object);

            sut.ControllerContext = GetControllerContext();

            // Act
            var response = sut.GetCaseOutcome("EDC2E452-E032-43E9-A5D3-B0610CC0E28C").Result;

            // Assert
            var taskResult = response.Should().BeOfType<ActionResult<GetCaseOutcomeResponse>>().Subject;
            var notFoundResult = taskResult.Result.Should().BeAssignableTo<NotFoundObjectResult>().Subject;
            notFoundResult.Value.Should().BeAssignableTo<GetCaseOutcomeResponse>();
        }

        [Fact]
        public void GetCaseOutcome_When_Called_With_Invalid_Id_Returns_Expected_Type()
        {
            // Arrange
            var logger = new Mock<IMELogger>();
            var mapper = new Mock<IMapper>();
            var examination = new Examination
            {
                ExaminationId = Guid.NewGuid().ToString()
            };

            var usersRetrievalByEmailService = new Mock<IAsyncQueryHandler<UserRetrievalByEmailQuery, MeUser>>();
            var mockMeUser = new Mock<MeUser>();
            usersRetrievalByEmailService.Setup(service => service.Handle(It.IsAny<UserRetrievalByEmailQuery>()))
                .Returns(Task.FromResult(mockMeUser.Object));

            var closeCaseService = new Mock<IAsyncQueryHandler<CloseCaseQuery, string>>();
            var coronerReferralService = new Mock<IAsyncQueryHandler<CoronerReferralQuery, string>>();

            var examinationRetrievalService = new Mock<IAsyncQueryHandler<ExaminationRetrievalQuery, Examination>>();
            examinationRetrievalService.Setup(service => service.Handle(It.IsAny<ExaminationRetrievalQuery>()))
                .Returns(Task.FromResult(examination));

            var saveOutstandingCaseItems = new Mock<IAsyncQueryHandler<SaveOutstandingCaseItemsQuery, string>>();

            var sut = new CaseOutcomeController(
                logger.Object,
                mapper.Object,
                coronerReferralService.Object,
                closeCaseService.Object,
                examinationRetrievalService.Object,
                saveOutstandingCaseItems.Object,
                usersRetrievalByEmailService.Object);

            sut.ControllerContext = GetControllerContext();

            // Act
            var response = sut.GetCaseOutcome("InvalidExaminationId").Result;

            // Assert
            var taskResult = response.Should().BeOfType<ActionResult<GetCaseOutcomeResponse>>().Subject;
            var notFoundResult = taskResult.Result.Should().BeAssignableTo<BadRequestObjectResult>().Subject;
            notFoundResult.Value.Should().BeAssignableTo<GetCaseOutcomeResponse>();
        }

        [Fact]
        public void GetCaseOutcome_When_Called_With_Valid_Id_Returns_Expected_Type()
        {
            // Arrange
            var logger = new Mock<IMELogger>();
            var mapper = new Mock<IMapper>();
            var examination = new Examination
            {
                ExaminationId = Guid.NewGuid().ToString()
            };

            var usersRetrievalByEmailService = new Mock<IAsyncQueryHandler<UserRetrievalByEmailQuery, MeUser>>();
            var mockMeUser = new Mock<MeUser>();
            usersRetrievalByEmailService.Setup(service => service.Handle(It.IsAny<UserRetrievalByEmailQuery>()))
                .Returns(Task.FromResult(mockMeUser.Object));

            var closeCaseService = new Mock<IAsyncQueryHandler<CloseCaseQuery, string>>();
            var coronerReferralService = new Mock<IAsyncQueryHandler<CoronerReferralQuery, string>>();

            var examinationRetrievalService = new Mock<IAsyncQueryHandler<ExaminationRetrievalQuery, Examination>>();
            examinationRetrievalService.Setup(service => service.Handle(It.IsAny<ExaminationRetrievalQuery>()))
                .Returns(Task.FromResult(examination));

            var saveOutstandingCaseItems = new Mock<IAsyncQueryHandler<SaveOutstandingCaseItemsQuery, string>>();

            var sut = new CaseOutcomeController(
                logger.Object,
                mapper.Object,
                coronerReferralService.Object,
                closeCaseService.Object,
                examinationRetrievalService.Object,
                saveOutstandingCaseItems.Object,
                usersRetrievalByEmailService.Object);

            sut.ControllerContext = GetControllerContext();

            // Act
            var response = sut.GetCaseOutcome(examination.ExaminationId).Result;

            // Assert
            var taskResult = response.Should().BeOfType<ActionResult<GetCaseOutcomeResponse>>().Subject;
            var okResult = taskResult.Result.Should().BeAssignableTo<OkObjectResult>().Subject;
        }

        [Fact]
        public async void PutOutstandingCaseItems_When_Called_With_No_Case_Id_Returns_Bad_Request()
        {
            // Arrange
            var logger = new Mock<IMELogger>();
            var mapper = new Mock<IMapper>();
            var usersRetrievalByEmailService = new Mock<IAsyncQueryHandler<UserRetrievalByEmailQuery, MeUser>>();
            var closeCaseService = new Mock<IAsyncQueryHandler<CloseCaseQuery, string>>();
            var coronerReferralService = new Mock<IAsyncQueryHandler<CoronerReferralQuery, string>>();
            var examinationRetrievalService = new Mock<IAsyncQueryHandler<ExaminationRetrievalQuery, Examination>>();
            var saveOutstandingCaseItems = new Mock<IAsyncQueryHandler<SaveOutstandingCaseItemsQuery, string>>();

            var sut = new CaseOutcomeController(
                    logger.Object,
                    mapper.Object,
                    coronerReferralService.Object,
                    closeCaseService.Object,
                    examinationRetrievalService.Object,
                    saveOutstandingCaseItems.Object,
                    usersRetrievalByEmailService.Object);

            sut.ControllerContext = GetControllerContext();

            var putOutstandingCaseItemsRequest = new PutOutstandingCaseItemsRequest
            {
                MCCDIssued = true,
                CremationFormStatus = CremationFormStatus.Yes,
                GPNotifiedStatus = GPNotified.GPNotified
            };

            // Act
            var response = await sut.PutOutstandingCaseItems(string.Empty, putOutstandingCaseItemsRequest);

            // Assert
            response.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async void PutOutstandingCaseItems_When_Called_With_Invalid_Case_Id_Returns_Bad_Request()
        {
            // Arrange
            var logger = new Mock<IMELogger>();
            var mapper = new Mock<IMapper>();
            var usersRetrievalByEmailService = new Mock<IAsyncQueryHandler<UserRetrievalByEmailQuery, MeUser>>();
            var closeCaseService = new Mock<IAsyncQueryHandler<CloseCaseQuery, string>>();
            var coronerReferralService = new Mock<IAsyncQueryHandler<CoronerReferralQuery, string>>();
            var examinationRetrievalService = new Mock<IAsyncQueryHandler<ExaminationRetrievalQuery, Examination>>();
            var saveOutstandingCaseItems = new Mock<IAsyncQueryHandler<SaveOutstandingCaseItemsQuery, string>>();

            var sut = new CaseOutcomeController(
                logger.Object,
                mapper.Object,
                coronerReferralService.Object,
                closeCaseService.Object,
                examinationRetrievalService.Object,
                saveOutstandingCaseItems.Object,
                usersRetrievalByEmailService.Object);

            sut.ControllerContext = GetControllerContext();

            var putOutstandingCaseItemsRequest = new PutOutstandingCaseItemsRequest
            {
                MCCDIssued = true,
                CremationFormStatus = CremationFormStatus.Yes,
                GPNotifiedStatus = GPNotified.GPNotified
            };

            // Act
            var response = await sut.PutOutstandingCaseItems("invalidCaseId", putOutstandingCaseItemsRequest);

            // Assert
            response.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async void PutOutstandingCaseItems_When_Called_With_Valid_Examination_Id_Returns_Ok()
        {
            // Arrange
            var logger = new Mock<IMELogger>();
            var mapper = new Mock<IMapper>();
            var examination = new Examination
            {
                ExaminationId = Guid.NewGuid().ToString()
            };

            var mockMeUser = new Mock<MeUser>();
            var usersRetrievalByEmailService = new Mock<IAsyncQueryHandler<UserRetrievalByEmailQuery, MeUser>>();
            usersRetrievalByEmailService.Setup(service => service.Handle(It.IsAny<UserRetrievalByEmailQuery>()))
                .Returns(Task.FromResult(mockMeUser.Object));

            var closeCaseService = new Mock<IAsyncQueryHandler<CloseCaseQuery, string>>();
            closeCaseService.Setup(service => service.Handle(It.IsAny<CloseCaseQuery>()))
                .Returns(Task.FromResult("test")).Verifiable();

            var coronerReferralService = new Mock<IAsyncQueryHandler<CoronerReferralQuery, string>>();
            var saveOutstandingCaseItems = new Mock<IAsyncQueryHandler<SaveOutstandingCaseItemsQuery, string>>();

            var examinationRetrievalService = new Mock<IAsyncQueryHandler<ExaminationRetrievalQuery, Examination>>();
            examinationRetrievalService.Setup(service => service.Handle(It.IsAny<ExaminationRetrievalQuery>()))
                .Returns(Task.FromResult(examination)).Verifiable();

            var sut = new CaseOutcomeController(
                logger.Object,
                mapper.Object,
                coronerReferralService.Object,
                closeCaseService.Object,
                examinationRetrievalService.Object,
                saveOutstandingCaseItems.Object,
                usersRetrievalByEmailService.Object);

            sut.ControllerContext = GetControllerContext();

            var putOutstandingCaseItemsRequest = new PutOutstandingCaseItemsRequest
            {
                MCCDIssued = true,
                CremationFormStatus = CremationFormStatus.Yes,
                GPNotifiedStatus = GPNotified.GPNotified
            };

            // Act
            var response = await sut.PutOutstandingCaseItems(examination.ExaminationId, putOutstandingCaseItemsRequest);

            // Assert
            var okResult = response.Should().BeAssignableTo<OkResult>().Subject;
        }

        [Fact]
        public async void PutCoronerReferral_When_Called_With_No_Case_Id_Returns_Bad_Request()
        {
            // Arrange
            var logger = new Mock<IMELogger>();
            var mapper = new Mock<IMapper>();
            var usersRetrievalByEmailService = new Mock<IAsyncQueryHandler<UserRetrievalByEmailQuery, MeUser>>();
            var closeCaseService = new Mock<IAsyncQueryHandler<CloseCaseQuery, string>>();
            var coronerReferralService = new Mock<IAsyncQueryHandler<CoronerReferralQuery, string>>();
            var examinationRetrievalService = new Mock<IAsyncQueryHandler<ExaminationRetrievalQuery, Examination>>();
            var saveOutstandingCaseItems = new Mock<IAsyncQueryHandler<SaveOutstandingCaseItemsQuery, string>>();

            var sut = new CaseOutcomeController(
                    logger.Object,
                    mapper.Object,
                    coronerReferralService.Object,
                    closeCaseService.Object,
                    examinationRetrievalService.Object,
                    saveOutstandingCaseItems.Object,
                    usersRetrievalByEmailService.Object);

            sut.ControllerContext = GetControllerContext();

            // Act
            var response = await sut.PutCoronerReferral(string.Empty);

            // Assert
            response.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async void PutCoronerReferral_When_Called_With_Invalid_Case_Id_Returns_Bad_Request()
        {
            // Arrange
            var logger = new Mock<IMELogger>();
            var mapper = new Mock<IMapper>();
            var usersRetrievalByEmailService = new Mock<IAsyncQueryHandler<UserRetrievalByEmailQuery, MeUser>>();
            var closeCaseService = new Mock<IAsyncQueryHandler<CloseCaseQuery, string>>();
            var coronerReferralService = new Mock<IAsyncQueryHandler<CoronerReferralQuery, string>>();
            var examinationRetrievalService = new Mock<IAsyncQueryHandler<ExaminationRetrievalQuery, Examination>>();
            var saveOutstandingCaseItems = new Mock<IAsyncQueryHandler<SaveOutstandingCaseItemsQuery, string>>();

            var sut = new CaseOutcomeController(
                logger.Object,
                mapper.Object,
                coronerReferralService.Object,
                closeCaseService.Object,
                examinationRetrievalService.Object,
                saveOutstandingCaseItems.Object,
                usersRetrievalByEmailService.Object);

            sut.ControllerContext = GetControllerContext();

            // Act
            var response = await sut.PutCoronerReferral("invalidCaseId");

            // Assert
            response.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async void PutCoronerReferral_When_Called_With_Valid_Examination_Id_Returns_Ok()
        {
            // Arrange
            var logger = new Mock<IMELogger>();
            var mapper = new Mock<IMapper>();
            var examination = new Examination
            {
                ExaminationId = Guid.NewGuid().ToString()
            };

            var mockMeUser = new Mock<MeUser>();
            var usersRetrievalByEmailService = new Mock<IAsyncQueryHandler<UserRetrievalByEmailQuery, MeUser>>();
            usersRetrievalByEmailService.Setup(service => service.Handle(It.IsAny<UserRetrievalByEmailQuery>()))
                .Returns(Task.FromResult(mockMeUser.Object));

            var closeCaseService = new Mock<IAsyncQueryHandler<CloseCaseQuery, string>>();
            closeCaseService.Setup(service => service.Handle(It.IsAny<CloseCaseQuery>()))
                .Returns(Task.FromResult("test")).Verifiable();

            var coronerReferralService = new Mock<IAsyncQueryHandler<CoronerReferralQuery, string>>();
            var saveOutstandingCaseItems = new Mock<IAsyncQueryHandler<SaveOutstandingCaseItemsQuery, string>>();

            var examinationRetrievalService = new Mock<IAsyncQueryHandler<ExaminationRetrievalQuery, Examination>>();
            examinationRetrievalService.Setup(service => service.Handle(It.IsAny<ExaminationRetrievalQuery>()))
                .Returns(Task.FromResult(examination)).Verifiable();

            var sut = new CaseOutcomeController(
                logger.Object,
                mapper.Object,
                coronerReferralService.Object,
                closeCaseService.Object,
                examinationRetrievalService.Object,
                saveOutstandingCaseItems.Object,
                usersRetrievalByEmailService.Object);

            sut.ControllerContext = GetControllerContext();

            // Act
            var response = await sut.PutCoronerReferral(examination.ExaminationId);

            // Assert
            var okResult = response.Should().BeAssignableTo<OkResult>().Subject;
        }

        [Fact]
        public async void PutCloseCase_When_Called_With_No_Case_Id_Returns_Bad_Request()
        {
            // Arrange
            var logger = new Mock<IMELogger>();
            var mapper = new Mock<IMapper>();
            var usersRetrievalByEmailService = new Mock<IAsyncQueryHandler<UserRetrievalByEmailQuery, MeUser>>();
            var closeCaseService = new Mock<IAsyncQueryHandler<CloseCaseQuery, string>>();
            var coronerReferralService = new Mock<IAsyncQueryHandler<CoronerReferralQuery, string>>();
            var examinationRetrievalService = new Mock<IAsyncQueryHandler<ExaminationRetrievalQuery, Examination>>();
            var saveOutstandingCaseItems = new Mock<IAsyncQueryHandler<SaveOutstandingCaseItemsQuery, string>>();

        var sut = new CaseOutcomeController(
                logger.Object,
                mapper.Object,
                coronerReferralService.Object,
                closeCaseService.Object,
                examinationRetrievalService.Object,
                saveOutstandingCaseItems.Object,
                usersRetrievalByEmailService.Object);

            sut.ControllerContext = GetControllerContext();

            // Act
            var response = await sut.PutCloseCase(string.Empty);

            // Assert
            response.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async void PutCloseCase_When_Called_With_Invalid_Case_Id_Returns_Bad_Request()
        {
            // Arrange
            var logger = new Mock<IMELogger>();
            var mapper = new Mock<IMapper>();
            var usersRetrievalByEmailService = new Mock<IAsyncQueryHandler<UserRetrievalByEmailQuery, MeUser>>();
            var closeCaseService = new Mock<IAsyncQueryHandler<CloseCaseQuery, string>>();
            var coronerReferralService = new Mock<IAsyncQueryHandler<CoronerReferralQuery, string>>();
            var examinationRetrievalService = new Mock<IAsyncQueryHandler<ExaminationRetrievalQuery, Examination>>();
            var saveOutstandingCaseItems = new Mock<IAsyncQueryHandler<SaveOutstandingCaseItemsQuery, string>>();

            var sut = new CaseOutcomeController(
                logger.Object,
                mapper.Object,
                coronerReferralService.Object,
                closeCaseService.Object,
                examinationRetrievalService.Object,
                saveOutstandingCaseItems.Object,
                usersRetrievalByEmailService.Object);

            sut.ControllerContext = GetControllerContext();

            // Act
            var response = await sut.PutCloseCase("invalidCaseId");

            // Assert
            response.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async void PutCloseCase_When_Called_With_Valid_Examination_Id_Returns_Ok()
        {
            // Arrange
            var logger = new Mock<IMELogger>();
            var mapper = new Mock<IMapper>();
            var examination = new Examination
            {
                ExaminationId = Guid.NewGuid().ToString()
            };

            var mockMeUser = new Mock<MeUser>();
            var usersRetrievalByEmailService = new Mock<IAsyncQueryHandler<UserRetrievalByEmailQuery, MeUser>>();
            usersRetrievalByEmailService.Setup(service => service.Handle(It.IsAny<UserRetrievalByEmailQuery>()))
                .Returns(Task.FromResult(mockMeUser.Object));

            var closeCaseService = new Mock<IAsyncQueryHandler<CloseCaseQuery, string>>();
            closeCaseService.Setup(service => service.Handle(It.IsAny<CloseCaseQuery>()))
                .Returns(Task.FromResult("test")).Verifiable();

            var coronerReferralService = new Mock<IAsyncQueryHandler<CoronerReferralQuery, string>>();
            var saveOutstandingCaseItems = new Mock<IAsyncQueryHandler<SaveOutstandingCaseItemsQuery, string>>();

            var examinationRetrievalService = new Mock<IAsyncQueryHandler<ExaminationRetrievalQuery, Examination>>();
            examinationRetrievalService.Setup(service => service.Handle(It.IsAny<ExaminationRetrievalQuery>()))
                .Returns(Task.FromResult(examination)).Verifiable();

            var sut = new CaseOutcomeController(
                logger.Object,
                mapper.Object,
                coronerReferralService.Object,
                closeCaseService.Object,
                examinationRetrievalService.Object,
                saveOutstandingCaseItems.Object,
                usersRetrievalByEmailService.Object);

            sut.ControllerContext = GetControllerContext();

            // Act
            var response = await sut.PutCloseCase(examination.ExaminationId);

            // Assert
            var okResult = response.Should().BeAssignableTo<OkResult>().Subject;
        }

        private ControllerContext GetControllerContext()
        {
            return new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, "username")
                    }, "someAuthTypeName"))
                }
            };
        }

    }
}