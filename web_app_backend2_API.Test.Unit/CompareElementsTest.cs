using System.Collections.Generic;
using Castle.Core.Internal;
using NSubstitute;
using NUnit.Framework;
using web_app_backend2.Models;
using web_app_backend2_API;
using web_app_backend2_API.Interfaces;

namespace web_app_backend2_API_Unit.Test
{
    [TestFixture]
    public class CompareElementsTest
    {
        private ICompareElements uut;
        private List<Heatmap> items;

        private Heatmap h1 = new Heatmap
        {
            x = 3,
            y = 4,
            Value = 1,
            HeatmapID = 1
        };

        private Heatmap h2 = new Heatmap
        {
            x = 42,
            y = 62,
            Value = 1,
            HeatmapID = 2
        };

        private Heatmap h3 = new Heatmap
        {
            x = 32,
            y = 3,
            Value = 1,
            HeatmapID = 3
        };

        private Heatmap h4 = new Heatmap
        {
            x = 17,
            y = 55,
            Value = 1,
            HeatmapID = 4
        };

        private Heatmap h5 = new Heatmap
        {
            x = 3,
            y = 4,
            Value = 1,
            HeatmapID = 5
        };

        private Heatmap h6 = new Heatmap
        {
            x = 17,
            y = 55,
            Value = 1,
            HeatmapID = 6
        };

        private Heatmap h7 = new Heatmap
        {
            x = 17,
            y = 55,
            Value = 1,
            HeatmapID = 7
        };
        private Heatmap h8 = new Heatmap
        {
            x = 17,
            y = 55,
            Value = 1,
            HeatmapID = 8
        };


        [SetUp]
        public void Setup()
        {
            uut = new CompareElements();

            items = new List<Heatmap>();

        }

        [Test]
        public void noMatchingItem_inlist()
        {
            items.Add(h1);
            items.Add(h2);
            items.Add(h3);
            items.Add(h4);
            uut.CompareHeatmapValues(items);
            Assert.AreEqual(1, items[1].Value);
            items.Clear();
        }

        [Test]
        public void twoMatchingItems_inlist()
        {
            items.Add(h1);
            items.Add(h2);
            items.Add(h3);
            items.Add(h4);
            items.Add(h5);
            uut.CompareHeatmapValues(items);
            Assert.AreEqual(2, items[4].Value);
            items.Clear();

        }

        [Test]
        public void threeMatchingItem_inlist()
        {
            items.Add(h2);
            items.Add(h3);
            items.Add(h4);
            items.Add(h6);
            items.Add(h7);
            uut.CompareHeatmapValues(items);
            Assert.AreEqual(3, items[4].Value);
            items.Clear();
        }
        [Test]
        public void FourMatchingItem_inlist()
        {
            items.Add(h2);
            items.Add(h3);
            items.Add(h4);
            items.Add(h6);
            items.Add(h7);
            items.Add(h8);

            uut.CompareHeatmapValues(items);
            Assert.AreEqual(4, items[2].Value);
            items.Clear();
        }
    }

}