﻿using BlazorBattles.Server.Data;
using BlazorBattles.Server.Services;
using BlazorBattles.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBattles.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public UserController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        [HttpGet("getbananas")]
        public async Task<IActionResult> GetBananas()
        {
            var user = await _utilityService.GetUser();

            return Ok(user.Bananas);
        }

        [HttpPut("addbananas")]
        public async Task<IActionResult> AddBananas([FromBody] int bananas)
        {
            var user = await _utilityService.GetUser();
            user.Bananas += bananas;

            await _context.SaveChangesAsync();
            return Ok(user.Bananas);
        }

        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetLeaderboard()
        {
            var users = await _context.Users.Where(u => !u.IsDeleted && u.IsConfirmed).ToListAsync();

            users = users
                .OrderByDescending(u => u.Victories)
                .ThenBy(u => u.Defeats)
                .ThenBy(u => u.DateCreated)
                .ToList();

            int rank = 1;
            var response = users.Select(user => new UserStatistic
            {
                Rank = rank++,
                UserId = user.Id,
                Username = user.Username,
                Battles = user.Battles,
                Victories = user.Victories,
                Defeats = user.Defeats
            });

            return Ok(response);
        }
    }
}