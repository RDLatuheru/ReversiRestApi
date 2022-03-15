using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReversiRestApi.DAL;
using ReversiRestApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiRestApi.Controllers
{
    [Route("api/Spel")]
    [ApiController]
    public class SpelController : ControllerBase
    {
        private readonly ISpelRepository iRepository;

        public SpelController(ISpelRepository repository)
        {
            iRepository = repository;
        }


        // GET api/spel
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetSpelOmschrijvingenVanSpellenMetWachtendeSpeler()
        {
            return iRepository.GetSpellen().FindAll(x => x.Speler2Token == null).Select(x => $"Omschrijving: {x.Omschrijving} | Speler1Token: {x.Token}").ToList();
        }

        // GET api/spel/{spelToken}
        [HttpGet("{spelToken}")]
        public ActionResult<SpelInfo> GetSpelByToken(string spelToken)
        {
            Spel spel = iRepository.GetSpel(spelToken);

            return spel != null ? Ok(new SpelInfo(spel)) : NotFound();
        }

        // GET api/spel/speler1/{speler1Token}
        [HttpGet("speler1/{speler1Token}")]
        public ActionResult<SpelInfo> GetSpelBySpeler1Token(string speler1Token)
        {
            Spel spel = iRepository.GetSpelBySpeler1(speler1Token);

            return spel != null ? Ok(new SpelInfo(spel)) : NotFound();
        }

        // GET api/spel/Beurt
        [HttpGet("beurt")]
        public ActionResult<Kleur> GetBeurt(string spelToken)
        {
            Spel spel = iRepository.GetSpel(spelToken);

            return spel != null ? Ok(spel.AandeBeurt.ToString()) : NotFound();
        }

        // POST api/spel
        /// <summary>
        /// Initialises a new game.
        /// </summary>
        /// <param name="spelInit"> Provides game info: description and player1token </param>
        /// <returns> Game info </returns>
        [HttpPost]
        public ActionResult<SpelInfo> CreateSpel([FromBody]SpelInit spelInit)
        {
            Spel spel = new() { Speler1Token = spelInit.Speler1Token, Omschrijving = spelInit.Omschrijving, Token = CreateGUID() };

            if (spel == null) return BadRequest(spel);

            iRepository.AddSpel(spel);

            return Ok(new SpelInfo(spel));
        }

        // PUT api/spel/zet
        /// <summary>
        /// Checks the move of a player
        /// Mutates the game board if a player's provided move is valid
        /// Provides exception information incase a move is not valid.
        /// </summary>
        /// <param name="spelZet"> Provides info for a player's move </param>
        /// <returns> Status of the player's move: a valid move or an exception message</returns>
        [HttpPut("zet")]
        public ActionResult<string> DoeZet([FromBody] SpelZet spelZet)
        {
            Spel spel = iRepository.GetSpel(spelZet.SpelToken);

            if (spel == null) return NotFound();

            string status = "";

            try
            {
                if (spelZet.HasPassed)
                {
                    spel.Pas();
                    status = $"{spel.AandeBeurt} heeft gepast";
                }
                else
                {
                    spel.DoeZet(spelZet.Row, spelZet.Col);
                    status = $"{spel.AandeBeurt} heeft een zet gedaan";
                }
            }
            catch (Exception e)
            {

                status = e.Message;
            }

            return status;
        }

        // PUT api/spel/opgeven
        [HttpPut("opgeven")]
        public ActionResult<SpelStatus> GeefOp(string spelToken, string spelerToken)
        {
            Spel spel = iRepository.GetSpel(spelToken);

            if (spel == null) return NotFound();

            //TODO: Spel sluiten

            return Ok(new SpelStatus(spel));
        }


        /// <summary>
        /// Creates a GUID
        /// GUID is checked against existing GUID's
        /// GUID is recreated until it is unique
        /// </summary>
        /// <returns> A unique GUID </returns>
        private string CreateGUID()
        {
            string token = "";

            bool isUnique = false;
            while (!isUnique)
            {
                token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                token = token.Replace("/", "q");    // slash mijden ivm het opvragen van een spel via een api obv het token
                token = token.Replace("+", "r");    // plus mijden ivm het opvragen van een spel via een api obv het token
                isUnique = iRepository.GetSpel(token) == null;
            }

            return token;
        }
    }
}
