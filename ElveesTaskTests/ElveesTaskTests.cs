using System.Collections.Generic;
using System.Diagnostics;
using ElveesTaskClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElveesTaskTests
{
    [TestClass]
    public class ElveesTaskTests
    {
        [TestMethod]
        public void Task_5_4_Test()
        {
            var expectedLists = new List<List<int>>
            {
                new List<int>() { 1, 2, 4, 3, 5 },
                new List<int>() { 1, 2, 4, 5, 3 },
                new List<int>() { 1, 2, 5, 4, 3 },
                new List<int>() { 1, 2, 5, 3, 4 },
                new List<int>() { 1, 4, 2, 3, 5 },
                new List<int>() { 1, 4, 2, 5, 3 },
                new List<int>() { 1, 4, 5, 3, 2 },
                new List<int>() { 1, 4, 5, 2, 3 },
                new List<int>() { 1, 5, 2, 3, 4 },
                new List<int>() { 1, 5, 2, 4, 3 },
                new List<int>() { 1, 5, 4, 3, 2 },
                new List<int>() { 1, 5, 4, 2, 3 },
                new List<int>() { 2, 1, 4, 3, 5 },
                new List<int>() { 2, 1, 4, 5, 3 },
                new List<int>() { 2, 1, 5, 4, 3 },
                new List<int>() { 2, 1, 5, 3, 4 },
                new List<int>() { 2, 4, 1, 3, 5 },
                new List<int>() { 2, 4, 1, 5, 3 },
                new List<int>() { 2, 4, 5, 3, 1 },
                new List<int>() { 2, 4, 5, 1, 3 },
                new List<int>() { 2, 5, 1, 3, 4 },
                new List<int>() { 2, 5, 1, 4, 3 },
                new List<int>() { 2, 5, 4, 3, 1 },
                new List<int>() { 2, 5, 4, 1, 3 },
                new List<int>() { 4, 2, 1, 3, 5 },
                new List<int>() { 4, 2, 1, 5, 3 },
                new List<int>() { 4, 2, 5, 1, 3 },
                new List<int>() { 4, 2, 5, 3, 1 },
                new List<int>() { 4, 1, 2, 3, 5 },
                new List<int>() { 4, 1, 2, 5, 3 },
                new List<int>() { 4, 1, 5, 3, 2 },
                new List<int>() { 4, 1, 5, 2, 3 },
                new List<int>() { 4, 5, 2, 3, 1 },
                new List<int>() { 4, 5, 2, 1, 3 },
                new List<int>() { 4, 5, 1, 3, 2 },
                new List<int>() { 4, 5, 1, 2, 3 },
                new List<int>() { 5, 2, 4, 3, 1 },
                new List<int>() { 5, 2, 4, 1, 3 },
                new List<int>() { 5, 2, 1, 4, 3 },
                new List<int>() { 5, 2, 1, 3, 4 },
                new List<int>() { 5, 4, 2, 3, 1 },
                new List<int>() { 5, 4, 2, 1, 3 },
                new List<int>() { 5, 4, 1, 3, 2 },
                new List<int>() { 5, 4, 1, 2, 3 },
                new List<int>() { 5, 1, 2, 3, 4 },
                new List<int>() { 5, 1, 2, 4, 3 },
                new List<int>() { 5, 1, 4, 3, 2 },
                new List<int>() { 5, 1, 4, 2, 3 },
            };

            var graph = new MetroGraph<int>(5, 4);
            graph.AddConnection(3, 1);
            graph.AddConnection(3, 2);
            graph.AddConnection(3, 4);
            graph.AddConnection(3, 5);

            var resultList = graph.GetCloseSequence();

            bool result = false;
            foreach (var list in expectedLists)
            {
                result |= CompareOrderedLists(list, resultList);
                if (result)
                    break;
            }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SingleNodeTest()
        {
            var expectedList = new List<int>() { 1 };

            var graph = new MetroGraph<int>(1, 0);
            graph.AddNode(1);

            var resultList = graph.GetCloseSequence();

            Assert.IsTrue(CompareOrderedLists(expectedList, resultList));
        }

        [TestMethod]
        public void TriangleShapeTest()
        {
            var expectedLists = new List<List<int>>()
            {
                new List<int>() { 1, 2, 3 },
                new List<int>() { 1, 3, 2 },
                new List<int>() { 2, 1, 3 },
                new List<int>() { 2, 3, 1 },
                new List<int>() { 3, 1, 2 },
                new List<int>() { 3, 2, 1 }
            };

            var graph = new MetroGraph<int>(3, 3);
            graph.AddConnection(1, 2);
            graph.AddConnection(2, 3);
            graph.AddConnection(3, 1);

            var resultList = graph.GetCloseSequence();

            bool result = false;
            foreach (var list in expectedLists)
            {
                result |= CompareOrderedLists(list, resultList);
                if (result)
                    break;
            }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ManyNodesTest()
        {
            var graph = new MetroGraph<int>(1000, 0);

            for (int i = 1; i < 1000; i++)
                for (int j = i+1; j <= 1000; j++)
                    graph.AddConnection(i, j);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            graph.GetCloseSequence();

            sw.Stop();

            Assert.IsTrue(sw.ElapsedMilliseconds < 1000);
        }

        private bool CompareOrderedLists(List<int> listA, List<int> listB)
        {
            if (listA.Count != listB.Count)
                return false;

            for (int i = 0; i < listA.Count; i++)
            {
                if (listA[i] != listB[i])
                    return false;
            }

            return true;
        }
    }
}
