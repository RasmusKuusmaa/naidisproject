using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NaidisprojektAPi.Models;
using NaidisprojektAPi.Repository;
using System.Runtime.CompilerServices;

namespace NaidisprojektAPi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository userRepository;
        public UserController(UserRepository repo)
        {
            userRepository = repo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> ValidateUser([FromBody] ValidateUserRequest request) 
        {
            var user = await userRepository.Validateuser(request.Email, request.Password);
            if (user is null)
                return NotFound("Invalid credentials");
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var res = await userRepository.RegisterUser(request.Name, request.Password, request.Email);
            if (res == false)
                return BadRequest();
            return Ok();
        }

        [HttpGet("listings")]
        public async Task<IActionResult> GetAllListings()
        {
            var res = await userRepository.GetAllListings();
            return Ok(res);
        }
        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var res = await userRepository.GetAllCategories();
            return Ok(res);
        }
        [HttpPost("listings/new")]
        public async Task<IActionResult> Addnewlisting([FromBody] ListingResponse request)
        {
            var res = await userRepository.Addnewlisting(request);
            if (res == true)
                return Ok();
            return BadRequest();
        }
        [HttpGet("listings/{id}")]
        public async Task<IActionResult> GetListingsbyUser(int id)
        {
            var res = await userRepository.GetListingsByUser(id);
            return Ok(res);
        }
        [HttpPost("listings/favorites/add")]
        public async Task<IActionResult> AddNewListing([FromBody] NewFavoriteListing request)
        {
            var res = await userRepository.FavoriteAnListing(request.UserId, request.ListingId);
            if (res == true)
                return Ok();
            return BadRequest();
        }

        [HttpGet("listings/favorites/{id}")]
        public async Task<IActionResult> GetFavoritesListings(int id)
        {
            var res = await userRepository.GetFavoriteListings(id);
            return Ok(res);
        }
        [HttpDelete("listings/favorites/remove/{id}")]
        public async Task<IActionResult> RemovelistingFromFavorites(int id)
        {
            var res = await userRepository.RemoveListingFromFavorites(id);
            if (res == true)
                return Ok();
            return BadRequest();
        }

        [HttpDelete("listings/remove/{id}")]
        public async Task<IActionResult> DeleteListing(int id)
        {
            var res = await userRepository.DeleteListing(id);
            if (res == true)
                return Ok();
            return BadRequest();
        }

        [HttpPut("user/{id}")]
        public async Task<IActionResult> UpdateUserInformation([FromBody] ValidateUserRespoonse request)
        {
            var res = await userRepository.UpdateUserInformation(request.UserId, request.UserName, request.Email);
            if (res == true)
                return Ok();
            return NotFound();
        }
    }
}
