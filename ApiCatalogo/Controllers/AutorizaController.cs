using System;
using System.Threading.Tasks;
using ApiCatalogo.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizaController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AutorizaController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Log de Entrada do Usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> LogUsuario()
        {
            return "AutorizaController :: Acessado em : " + DateTime.Now.ToLongDateString();
        }

        /// <summary>
        /// Registrar Usuário
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("registrar")]
        public async Task<ActionResult> RegistrarUsuario([FromBody] UsuarioDTO dto)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                EmailConfirmed = true
            };

            //Do tipo IdentityResult
            var result = await _userManager.CreateAsync(user, dto.Senha);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _signInManager.SignInAsync(user, false);
            return Ok();
        }

        /// <summary>
        /// Login do Usuário
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UsuarioDTO dto)
        {
            //Verifica as credenciais do usuario e retorna um valor do tipo SigninResult
            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Senha, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login inválido...");
                return BadRequest(ModelState);
            }
        }



    }
}