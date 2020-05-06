using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiCatalogo.DTO;
using ApiCatalogo.DTO.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizaController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AutorizaController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
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
            return Ok(GerarToken(dto));
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
                return Ok(GerarToken(dto));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login inválido...");
                return BadRequest(ModelState);
            }
        }


        private object GerarToken(UsuarioDTO dto)
        {
            //Define as declarações do Usuario
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, dto.Email),
                new Claim("meuPet", "Nina"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //Pega a chave no AppSettings.Development.Json
            byte[] bytes = Encoding.UTF8.GetBytes(_configuration["JwtKey:Key"]);

            //Gera uma chave com base no algoritmo simetrico
            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);

            //Gera a assinatura digital do Token usando o algoritmo Hmac e a chave privada
            SigningCredentials credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Tempo de Expiracao do Token
            string tempoExpiracao = _configuration["TokenSettings:ExpireHours"];
            DateTime expiracao = DateTime.UtcNow.AddHours(double.Parse(tempoExpiracao));

            // Classe que representa um Token JWT e gera ele
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["TokenSettings:Issuer"],
                audience: _configuration["TokenSettings:Audience"],
                claims: claims,
                expires: expiracao,
                signingCredentials: credenciais);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(token);

            //Retorna os dados com o Token e informações
            return new UsuarioToken()
            {
                Autenticado = true,
                Token = tokenString,
                DataExpiracao = expiracao,
                Mensagem = "Token Criado com Sucesso"

            };
        }

    }
}