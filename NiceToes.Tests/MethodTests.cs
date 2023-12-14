using NiceToes.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace NiceToes.Tests
{
    public class MethodTests
    {
        [Fact]
        void TestMaxFileSizeCheck()
        {
            // Setup
            object size = 5;

            var fileSizeChecker = new MaxFileSizeAttribute(1000);

            // Act
            var actualResult = fileSizeChecker.GetValidationResult(size, new ValidationContext(""));

            // Assert
            Assert.Equal(actualResult, ValidationResult.Success);
        }
    }
}
