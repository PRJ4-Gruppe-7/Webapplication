using System.Collections.Generic;
using Castle.Core.Internal;
using NUnit.Framework;
using web_app_backend2_API;
using web_app_backend2_API.Interfaces;

namespace web_app_backend2_API_Unit.Test
{
    [TestFixture]
    public class CheckEachStringTest
    {
        private ICheckEachString uut;
        private List<string> line1;
        private List<string> line2;
        private List<string> line3;
        private string[] stringlist;
        private string x1;
        private string y1;
        private string h1;
       

        [SetUp]
        public void setSetup()
        {
            uut = new CheckEachString();
            line1 = new List<string>();
            line2 = new List<string>();
            line3 = new List<string>();

            x1 = "X : 1";
            y1 = "Y : 1";
            h1 = "HEAT : 1";
           

        }

        [Test]
        public void ItemAddedToLine1()
        {

            stringlist = new[] { x1 };
            uut.CheckStringHeatmap(stringlist,line1,line2,line3);

            Assert.That(line1.IsNullOrEmpty());
        }

        [Test]
        public void ItemAddedToLine2()
        {

            stringlist = new[] { y1 };
            uut.CheckStringHeatmap(stringlist, line1, line2, line3);

            Assert.That(line2.IsNullOrEmpty());
        }

        [Test]
        public void ItemAddedToLine3()
        {

            stringlist = new[] { h1 };
            uut.CheckStringHeatmap(stringlist, line1, line2, line3);

            Assert.That(line3.IsNullOrEmpty());
        }

        [Test]
        public void ItemAddedToAllLines()
        {

            stringlist = new[] { x1,y1,h1 };
            uut.CheckStringHeatmap(stringlist, line1, line2, line3);

            Assert.That(line1.IsNullOrEmpty());
            Assert.That(line2.IsNullOrEmpty());
            Assert.That(line3.IsNullOrEmpty());
        }

        [Test]
        public void ItemAddedToOnly_line1_Line2()
        {

            stringlist = new[] { x1, y1};
            uut.CheckStringHeatmap(stringlist, line1, line2, line3);

            Assert.That(line3.IsNullOrEmpty());
        }

        [Test]
        public void ItemAddedToOnly_line2_Line3()
        {

            stringlist = new[] { y1, h1 };
            uut.CheckStringHeatmap(stringlist, line1, line2, line3);

            Assert.That(line1.IsNullOrEmpty());
        }

        [Test]
        public void ItemAddedToOnly_line1_Line3()
        {

            stringlist = new[] { x1, h1 };
            uut.CheckStringHeatmap(stringlist, line1, line2, line3);

            Assert.That(line2.IsNullOrEmpty());
        }
    }
}