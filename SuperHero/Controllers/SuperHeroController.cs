using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
                {
                new SuperHero {
                    Id = 1,
                    Name = "Spider Man",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Place = "New York"
                }
            };  

        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        { _context = context; }

        //TODO: below methods are driven using hardcoded demo list
        //[HttpGet]
        //public async Task<ActionResult<List<SuperHero>>> Get()
        //{
        //    return Ok(heroes);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<SuperHero>> Get(int id)
        //{
        //    var hero = heroes.Find(h => h.Id == id);
        //    if (hero == null)
        //        return BadRequest(id);
        //    return Ok(hero);
        //}

        //[HttpPost]
        //public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        //{
        //    heroes.Add(hero);
        //    return Ok(heroes);
        //}

        //[HttpPut]
        //public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero hero)
        //{
        //    var updateHero = heroes.Find(h => h.Id == hero.Id);
        //    if (hero == null)
        //        return BadRequest(hero);
        //    updateHero.Name = hero.Name;
        //    updateHero.FirstName = hero.FirstName;
        //    updateHero.LastName = hero.LastName;
        //    return Ok(heroes);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        //{
        //    var hero = heroes.Find(h => h.Id == id);
        //    if (hero == null)
        //        return BadRequest(id);
        //    return Ok(heroes);
        //}

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest(id);
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero hero)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(hero.Id);
            if (dbHero == null)
                return BadRequest("Hero not found");

            dbHero.Name = hero.Name;
            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not found");
            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}  
