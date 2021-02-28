using System;
using AOOP.Sorting.Algorithms;
using AOOP.Sorting.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOOP.UnitTests
{
    [TestClass]
    public class AlgorithmsTests
    {
        #region Mocks
        private int[] intValues = new int[] { 2, 1, 5, 4, 3 };
        private int[] intExpected = new int[] { 1, 2, 3, 4, 5 };
        private double[] doubleValues = new double[] { 2.1, 1.1, 5.5, 4.3, 3.4 };
        private double[] doubleExpected = new double[] { 1.1, 2.1, 3.4, 4.3, 5.5 };

        #endregion
        #region BubbleSort
        [TestMethod]
        public void BubbleSort_ShouldSort_Int_Successful()
        {
            var algorithm = new BubbleSort<int>();

            var result = algorithm.Sort(intValues);

            CollectionAssert.AreEqual(intExpected, result as int[]);
        }

        [TestMethod]
        public void BubbleSort_ShouldSort_Double_Successful()
        {
            var algorithm = new BubbleSort<double>();

            var result = algorithm.Sort(doubleValues);

            CollectionAssert.AreEqual(doubleExpected, result as double[]);
        }

        [TestMethod]
        public void BubbleSort_ShouldReturnNull_OnNull()
        {
            var algorithm = new BubbleSort<int>();

            var result = algorithm.Sort(null);

            Assert.IsNull(result);
        }
        #endregion

        #region BucketSort
        [TestMethod]
        public void BucketSort_ShouldSort_Int_Successful()
        {
            var algorithm = new BucketSort<int>();

            var result = algorithm.Sort(intValues);

            CollectionAssert.AreEqual(intExpected, result as int[]);
        }

        [TestMethod]
        [ExpectedException(typeof(TypeNotAllowedException))]
        public void BucketSort_ShouldThrow_Double()
        {
            var algorithm = new BucketSort<double>();

            var result = algorithm.Sort(doubleValues);
        }

        [TestMethod]
        public void BucketSort_ShouldReturnNull_OnNull()
        {
            var algorithm = new BucketSort<int>();

            var result = algorithm.Sort(null);

            Assert.IsNull(result);
        }
        #endregion

        #region CountingSort
        [TestMethod]
        public void CountingSort_ShouldSort_Int_Successful()
        {
            var algorithm = new CountingSort<int>();

            var result = algorithm.Sort(intValues);

            CollectionAssert.AreEqual(intExpected, result as int[]);
        }

        [TestMethod]
        [ExpectedException(typeof(TypeNotAllowedException))]
        public void CountingSort_ShouldThrow_Double()
        {
            var algorithm = new CountingSort<double>();

            var result = algorithm.Sort(doubleValues);
        }

        [TestMethod]
        public void CountingSort_ShouldReturnNull_OnNull()
        {
            var algorithm = new CountingSort<int>();

            var result = algorithm.Sort(null);

            Assert.IsNull(result);
        }
        #endregion

        #region InsertionSort
        [TestMethod]
        public void InsertionSort_ShouldSort_Int_Successful()
        {
            var algorithm = new InsertionSort<int>();

            var result = algorithm.Sort(intValues);

            CollectionAssert.AreEqual(intExpected, result as int[]);
        }

        [TestMethod]
        public void InsertionSort_ShouldSort_Double_Successful()
        {
            var algorithm = new InsertionSort<double>();

            var result = algorithm.Sort(doubleValues);

            CollectionAssert.AreEqual(doubleExpected, result as double[]);
        }

        [TestMethod]
        public void InsertionSort_ShouldReturnNull_OnNull()
        {
            var algorithm = new InsertionSort<int>();

            var result = algorithm.Sort(null);

            Assert.IsNull(result);
        }
        #endregion

        #region MergeSort
        [TestMethod]
        public void MergeSort_ShouldSort_Int_Successful()
        {
            var algorithm = new MergeSort<int>();

            var result = algorithm.Sort(intValues);

            CollectionAssert.AreEqual(intExpected, result as int[]);
        }

        [TestMethod]
        public void MergeSort_ShouldSort_Double_Successful()
        {
            var algorithm = new MergeSort<double>();

            var result = algorithm.Sort(doubleValues);

            CollectionAssert.AreEqual(doubleExpected, result as double[]);
        }

        [TestMethod]
        public void MergeSort_ShouldReturnNull_OnNull()
        {
            var algorithm = new MergeSort<int>();

            var result = algorithm.Sort(null);

            Assert.IsNull(result);
        }
        #endregion

        #region QuickSort
        [TestMethod]
        public void QuickSort_ShouldSort_Int_Successful()
        {
            var algorithm = new QuickSort<int>();

            var result = algorithm.Sort(intValues);

            CollectionAssert.AreEqual(intExpected, result as int[]);
        }

        [TestMethod]
        public void QuickSort_ShouldSort_Double_Successful()
        {
            var algorithm = new QuickSort<double>();

            var result = algorithm.Sort(doubleValues);

            CollectionAssert.AreEqual(doubleExpected, result as double[]);
        }

        [TestMethod]
        public void QuickSort_ShouldReturnNull_OnNull()
        {
            var algorithm = new QuickSort<int>();

            var result = algorithm.Sort(null);

            Assert.IsNull(result);
        }
        #endregion
    }
}
