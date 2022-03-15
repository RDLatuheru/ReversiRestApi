using NUnit.Framework;
using ReversiRestApi;
using ReversiRestApi.DAL;
using ReversiRestApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class SpelRepository_Test
    {
        private SpelRepository _spelRepos;

        [SetUp]
        public void TestInit()
        {
            _spelRepos = new SpelRepository();
        }

        [Test]
        public void AddSpel_AddsNewSpel()
        {
            //add new game with specific id (invalid DB id for testing purposes)
            _spelRepos.AddSpel(new Spel() { ID = -1 });

            //find game by id
            var spelObj = _spelRepos.Spellen.Find(x => x.ID == -1);

            //obj should be defined
            Assert.NotNull(spelObj);
            //obj should be of type Spel
            Assert.IsTrue(spelObj is Spel);
        }

        [Test]
        public void GetSpellen_ReturnsAllSpellen()
        {
            //count existing games
            int count = _spelRepos.Spellen.Count;
            //increase number of games
            _spelRepos.AddSpel(new Spel());

            //Spellen.Count should be count + 1
            Assert.IsTrue(_spelRepos.Spellen.Count == count + 1);
        }

        [Test]
        public void GetSpel_ReturnsSpecificSpel()
        {
            //add game with token
            Spel spel = new Spel() { Token = "abc" };
            _spelRepos.AddSpel(spel);

            //GetSpel should return an object
            Spel expectedObj =  _spelRepos.GetSpel("abc");

            Assert.NotNull(expectedObj);
        }

        [Test]
        public void GetSpel_ReturnsNull()
        {
            //add game with token
            Spel spel = new Spel() { Token = "abc" };
            _spelRepos.AddSpel(spel);

            //GetSpel should return null
            Spel nullObj = _spelRepos.GetSpel("abcd");

            Assert.IsNull(nullObj);
        }

        [TestCase("result", true)]
        [TestCase("null", false)]
        public void GetSpelBySpeler1Token_Returns(string searchKey, bool expected)
        {
            //add game with Speler1Token
            _spelRepos.AddSpel(new Spel() { Speler1Token = "result" });

            //check result
            Spel spel = _spelRepos.GetSpelBySpeler1(searchKey);
            bool actual = spel != null;

            Assert.AreEqual(actual, expected);
        }
    }
}
