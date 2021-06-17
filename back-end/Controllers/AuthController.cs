using System.Threading.Tasks;
using basurapp.api.Data;
using basurapp.api.Dtos;
using basurapp.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace basurapp.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository privateRepository;
        private readonly IConfiguration privateConfiguration;

        public AuthController(IAuthRepository repository, IConfiguration configuration)
        {
            privateConfiguration = configuration;
            privateRepository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(UserForRegisterDto  userForRegister)
        {
            userForRegister.username = userForRegister.username.ToLower();

            var userToCreate = new BasurappUser
            {
                userName = userForRegister.username,
                realName = userForRegister.realName,
                lastName = userForRegister.lastName,
                phone = userForRegister.phone,
                role = userForRegister.role
            };

            ReturnCodeDto returnCode = new ReturnCodeDto();
            if ( await privateRepository.userExists(userForRegister.username))
            {
                returnCode.code=404;
                returnCode.data="the user already exists";
                return Ok(returnCode);
            }
            else
            {
                var createdUser = await privateRepository.registerUser(userToCreate, userForRegister.password);
                returnCode.code=200;
                returnCode.data=createdUser;
                return Ok(returnCode);                                              
            }
        }              

        [HttpPost("login")]
        public async Task<ActionResult> login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await privateRepository.login(userForLoginDto.username.ToLower(), userForLoginDto.password);

            
            ReturnCodeDto returnCode = new ReturnCodeDto();
            if ( userFromRepo != null)
            {
                returnCode.code=200;
                returnCode.data=userFromRepo;
                return Ok(userFromRepo);
            }
            else
            {
                returnCode.code=404;
                returnCode.data="wrong user or password";
                return Ok(returnCode);                                
            }
        }   
    } 
}