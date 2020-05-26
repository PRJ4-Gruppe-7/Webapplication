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


        [SetUp]
        public void Setup()
        {
            uut = new CompareElements();

            items = new List<Heatmap>();

            items.Add(h1);
            items.Add(h2);
            items.Add(h3);
            items.Add(h4);
            items.Add(h5);

        }

        [Test]
        public void oneMatchingItems_inlist()
        {
            uut.CompareHeatmapValues(items);
            Assert.AreEqual(3, items[4].Value);
        }

        [Test]
        public void noMatchingItem_inlist()
        {
            uut.CompareHeatmapValues(items);
            Assert.AreEqual(1, items[1].Value);
        }
    }

}