﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MedicalExaminer.API.Attributes;
using MedicalExaminer.API.Models.v1.Examinations;
using Moq;
using Xunit;

namespace MedicalExaminer.API.Tests.Attributes
{
    public class DateIsLessThanOrEqualToTests
    {
        private Dictionary<object, object> GetItemsDictionary()
        {
            return new Mock<Dictionary<object, object>>().Object;
        }

        [Fact]
        public async void EndDateFieldIsNotFoundOnObjectReturnsError()
        {
            // Arrange
            var startDate = new DateTime(2019, 01, 02);
            var endDateField = "DateOfDeath";
            var serviceProvider = new Mock<IServiceProvider>().Object;
            var validationContext = new ValidationContext(serviceProvider);

            var expectedError = $"Unable to find the end date field {endDateField} on the object";
            var sut = new DateIsLessThanOrEqualToNullsAllowed(endDateField);
            // Act
            var result = sut.GetValidationResult(startDate, validationContext);
            //Assert
            Assert.Equal(expectedError, result.ErrorMessage);
        }

        [Fact]
        public async void EndDateIsNullReturnsSuccess()
        {
            // Arrange
            var startDate = new DateTime(2019, 01, 02);
            var endDateField = "DateOfDeath";
            var serviceProvider = new Mock<IServiceProvider>().Object;
            var postRequest = new Mock<PostNewCaseRequest>();
            var validationContext = new ValidationContext(postRequest.Object, serviceProvider, GetItemsDictionary());
            var expectedResult = ValidationResult.Success;
            var sut = new DateIsLessThanOrEqualToNullsAllowed(endDateField);
            // Act
            var result = sut.GetValidationResult(startDate, validationContext);
            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void StartDateAndEndDateTheSameReturnsSuccess()
        {
            // Arrange
            var startDate = new DateTime(2019, 01, 02);
            var endDateField = "DateOfDeath";
            var serviceProvider = new Mock<IServiceProvider>().Object;
            var postRequest = new PostNewCaseRequest
            {
                DateOfDeath = startDate
            };
            var validationContext = new ValidationContext(postRequest, serviceProvider, GetItemsDictionary());
            var expectedResult = ValidationResult.Success;
            var sut = new DateIsLessThanOrEqualToNullsAllowed(endDateField);
            // Act
            var result = sut.GetValidationResult(startDate, validationContext);
            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void StartDateGreaterThanEndDateTheSameReturnsSuccess()
        {
            // Arrange
            var startDate = new DateTime(2019, 01, 02);
            var endDate = new DateTime(2019, 01, 01);
            var endDateField = "DateOfDeath";
            var serviceProvider = new Mock<IServiceProvider>().Object;
            var postRequest = new PostNewCaseRequest
            {
                DateOfDeath = endDate
            };
            var validationContext = new ValidationContext(postRequest, serviceProvider, GetItemsDictionary());
            var expectedResult = "The patient cannot have died before they were born";
            var sut = new DateIsLessThanOrEqualToNullsAllowed(endDateField);
            // Act
            var result = sut.GetValidationResult(startDate, validationContext);
            //Assert
            Assert.Equal(expectedResult, result.ErrorMessage);
        }

        [Fact]
        public async void StartDateIsNullReturnsSuccess()
        {
            // Arrange
            string startDate = null;
            var endDateField = "";
            var serviceProvider = new Mock<IServiceProvider>().Object;

            var expectedResult = ValidationResult.Success;
            var sut = new DateIsLessThanOrEqualToNullsAllowed(endDateField);
            // Act
            var result = sut.GetValidationResult(startDate, new ValidationContext(serviceProvider));
            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void StartDateLessThanEndDateTheSameReturnsSuccess()
        {
            // Arrange
            var startDate = new DateTime(2019, 01, 02);
            var endDate = new DateTime(2019, 01, 03);
            var endDateField = "DateOfDeath";
            var serviceProvider = new Mock<IServiceProvider>().Object;
            var postRequest = new PostNewCaseRequest
            {
                DateOfDeath = endDate
            };
            var validationContext = new ValidationContext(postRequest, serviceProvider, GetItemsDictionary());
            var expectedResult = ValidationResult.Success;
            var sut = new DateIsLessThanOrEqualToNullsAllowed(endDateField);
            // Act
            var result = sut.GetValidationResult(startDate, validationContext);
            //Assert
            Assert.Equal(expectedResult, result);
        }
    }
}