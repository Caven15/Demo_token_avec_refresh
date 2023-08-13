using Demo.API.Infrastructure;
using Demo.API.Mapper;
using Demo.API.models;
using Demo.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : Controller
    {
        private readonly IUtilisateurService _utilisateurService;

        public UtilisateurController(IUtilisateurService utilisateurService)
        {
            _utilisateurService = utilisateurService;
        }

        [Authorize]
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                UtilisateurViewModel utilisateur = _utilisateurService.GetUtilisateurById(id).BllToApi();
                if (utilisateur is null) 
                    return NotFound();


                return Ok(utilisateur);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
