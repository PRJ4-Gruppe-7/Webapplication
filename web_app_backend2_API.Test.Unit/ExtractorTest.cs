using System;
using NSubstitute;
using NUnit.Framework;
using web_app_backend2_API;
using web_app_backend2_API.Interfaces;

namespace web_app_backend2_API_Unit.Test
{
    [TestFixture]
    public class ExtractorTest
    {
        private IExtractor uut;

        [SetUp]
        public void Setup()
        {
            uut = new Extractor();
        }

        [Test]
        public void NoDigitInString()
        {
            string dummystring = "This is a unit test of extractor class";


            Assert.AreEqual(0, uut.ExtractFromString(dummystring));

        }

        [Test]
        public void OneDigitInString()
        {
            string dummystring = "II1II";

            Assert.AreEqual(1, uut.ExtractFromString(dummystring));

        }

        [Test]
        public void ManyDigitsInString()
        {
            string dummystring = "X: 13, Y: 52, Value: 3, HeatmapID: 223";

            Assert.AreEqual(13523223, uut.ExtractFromString(dummystring));

        }
    }
}