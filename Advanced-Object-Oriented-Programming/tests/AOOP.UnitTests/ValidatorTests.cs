using AOOP.Sorting.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOOP.UnitTests
{
    [TestClass]
    public class ValidatorTests
    {
        [TestMethod]
        public void Validator_ShouldReturnTrue()
        {
            var intValues = new int[] { 1, 2, 3, 4, 5 };
            var doubleValues = new double[] { 1.1, 2.1, 3.4, 4.3, 5.5 };

            var intResult = Helpers.Validate(intValues);
            var doubleResult = Helpers.Validate(doubleValues);

            Assert.IsTrue(intResult);
            Assert.IsTrue(doubleResult);
        }

        [TestMethod]
        public void Validator_ShouldReturnFalse()
        {
            var intValues = new int[] { 2, 1, 5, 4, 3 };
            var doubleValues = new double[] { 2.1, 1.1, 5.5, 4.3, 3.4 };

            var intResult = Helpers.Validate(intValues);
            var doubleResult = Helpers.Validate(doubleValues);

            Assert.IsFalse(intResult);
            Assert.IsFalse(doubleResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Validator_ShouldThrowException()
        {
            Helpers.Validate<int>(null);
        }
    }
}
