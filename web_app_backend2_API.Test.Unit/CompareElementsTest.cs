using System.Collections.Generic;
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

        [SetUp]
        public void Setup()
        {
            uut = new CompareElements();

            items = new List<Heatmap>();
            Heatmap h1 = new Heatmap
            {
                x = 3,
                y = 4,
                Value = 1,
                HeatmapID = 1
            };
            Heatmap h2 = new Heatmap
            {
                x = 42,
                y = 62,
                Value = 1,
                HeatmapID = 2
            };
            Heatmap h3 = new Heatmap
            {
                x = 32,
                y = 3,
                Value = 1,
                HeatmapID = 3
            };
            Heatmap h4 = new Heatmap
            {
                x = 17,
                y = 55,
                Value = 1,
                HeatmapID = 4
            };
            Heatmap h5 = new Heatmap
            {
                x = 3,
                y = 4,
                Value = 1,
                HeatmapID = 5
            };
            
            items.Add(h1);
            items.Add(h2);
            items.Add(h3);
            items.Add(h4);
            items.Add(h5);

        }

        [Test]
        public void OneMatchingItem()
        {
            uut.CompareHeatmapValues(items, items.Count);
            Assert.AreEqual(2,items[0].Value);
        }

        [Test]
        public void NoMatchingItem()
        {
            uut.CompareHeatmapValues(items, items.Count);
            Assert.AreNotEqual(2, items[1].Value);
        }
    }
}