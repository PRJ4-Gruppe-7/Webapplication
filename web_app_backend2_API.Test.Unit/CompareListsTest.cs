using System.Collections.Generic;
using NUnit.Framework;
using web_app_backend2.Models;
using web_app_backend2_API;
using web_app_backend2_API.Functionality;
using web_app_backend2_API.Interfaces;

namespace web_app_backend2_API_Unit.Test
{
    [TestFixture]
    public class CompareListsTest
    {
        private ICompareLists uut;
        private List<Heatmap> items1;
        private List<Heatmap> items2;
        private Heatmap r1 = new Heatmap
        {
            x = 0,
            y = 0,
            Value = 1,
            HeatmapID = 1
        };
        private Heatmap r2 = new Heatmap
        {
            x = 1,
            y = 1,
            Value = 1,
            HeatmapID = 2
        };
        private Heatmap r3 = new Heatmap
        {
            x = 2,
            y = 2,
            Value = 1,
            HeatmapID = 3
        };
        private Heatmap r4 = new Heatmap
        {
            x = 3,
            y = 3,
            Value = 1,
            HeatmapID = 4
        };
        private Heatmap r5 = new Heatmap
        {
            x = 4,
            y = 4,
            Value = 1,
            HeatmapID = 5
        };
        private Heatmap r6 = new Heatmap
        {
            x = 5,
            y = 5,
            Value = 1,
            HeatmapID = 6
        };
        private Heatmap r7 = new Heatmap
        {
            x = 6,
            y = 6,
            Value = 1,
            HeatmapID = 7
        };
        private Heatmap r8 = new Heatmap
        {
            x = 5,
            y = 5,
            Value = 1,
            HeatmapID = 12
        };


        private Heatmap h1 = new Heatmap
        {
            x = 2,
            y = 2,
            Value = 1,
            HeatmapID = 8
        };

        private Heatmap h2 = new Heatmap
        {
            x = 5,
            y = 5,
            Value = 1,
            HeatmapID = 9
        };
        private Heatmap h3 = new Heatmap
        {
            x = 5,
            y = 5,
            Value = 1,
            HeatmapID = 10
        };
        private Heatmap h4 = new Heatmap
        {
            x = 11,
            y = 11,
            Value = 1,
            HeatmapID = 11
        };



        [SetUp]
        public void Setup()
        {
            uut = new CompareLists();

            items1 = new List<Heatmap>();
            items2 = new List<Heatmap>();

            items1.Add(r1);
            items1.Add(r2);
            items1.Add(r3);
            items1.Add(r4);
            items1.Add(r5);
            items1.Add(r6);
            items1.Add(r7);

            items2.Add(h1);
            items2.Add(h2);
            items2.Add(h3);
            items2.Add(h4);



        }

        [Test]
        public void OneMatchingItem()
        {
            uut.CompareList(items1,items2);
            Assert.AreEqual(3, items2[0].Value);
        }

        [Test]
        public void noMatchingItem()
        {
            uut.CompareList(items1, items2);
            Assert.AreEqual(1, items2[3].Value);
        }


    }
}