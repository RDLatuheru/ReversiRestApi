using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ReversiRestApi.Controllers;
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
    public class SpelController_Test
    {
        SpelController _controller;
        SpelRepository _spelRepos;

        [SetUp]
        public void TestInit()
        {
            _spelRepos = new SpelRepository();
            _controller = new SpelController(_spelRepos);
        }

        [Test]
        public void CreateSpel_IncreasesGames()
        {
            int games = _spelRepos.Spellen.Count;

            SpelInit spelinit = new() { Omschrijving = "omschr", Speler1Token = "token1"};
            _controller.CreateSpel(spelinit);

            // (int) games should equal (actual - 1)
            Assert.AreEqual(games, _spelRepos.Spellen.Count - 1);
        }

        [TestCase("omschr1", "token1")]
        [TestCase("omschr2", "token2")]
        public void CreateGUID_ReturnsGUID(string omschrijving, string token)
        {
            SpelInit spelinit = new() { Omschrijving = omschrijving, Speler1Token = token };
            _controller.CreateSpel(spelinit);

            Assert.IsNotEmpty(_spelRepos.Spellen.Find(x => x.Speler1Token == token).Token);
        }

        [TestCase("token")]
        public void GetSpelByToken_ReturnsOk(string searchKey)
        {
            _spelRepos.AddSpel(new Spel() { Token = "token" });

            Assert.IsTrue(_controller.GetSpelByToken(searchKey).Result is OkObjectResult);
        }


        [TestCase("")]
        public void GetSpelByToken_ReturnsNotFound(string searchKey)
        {
            _spelRepos.AddSpel(new Spel() { Token = "token" });

            Assert.IsTrue(_controller.GetSpelByToken(searchKey).Result is NotFoundResult);
        }

        [TestCase("token", true)]
        public void GetSpelBySpeler1Token_ReturnsOk(string searchKey, bool expected)
        {
            _spelRepos.AddSpel(new Spel() { Speler1Token = "token" });

            bool actual = _controller.GetSpelBySpeler1Token(searchKey).Result is OkObjectResult;

            Assert.AreEqual(expected, actual);
        }


        [TestCase("", true)]
        public void GetSpelBySpeler1Token_ReturnsNotFound(string searchKey, bool expected)
        {
            _spelRepos.AddSpel(new Spel() { Speler1Token = "token" });

            bool actual = _controller.GetSpelBySpeler1Token(searchKey).Result is NotFoundResult;

            Assert.AreEqual(expected, actual);
        }
    }
}
