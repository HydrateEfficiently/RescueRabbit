using Microsoft.Data.Entity;
using RescueRabbit.Framework.Models.Motivation;
using RescueRabbit.Framework.Services;
using RescueRabbit.Framework.Utility;
using RescueRabbit.Services.Motivation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Services.Motivation
{
    public interface IMotivationService
    {
        Task<IEnumerable<MotivationPiece>> ListAsync(string userId);

        Task<MotivationPiece> CreateAsync(CreateMotivationPiece request);
    }

    public class MotivationService : IMotivationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IIdentityResolver _identityResolver;

        public MotivationService(
            ApplicationDbContext dbContext,
            IIdentityResolver identityResolver)
        {
            _dbContext = dbContext;
            _identityResolver = identityResolver;
        }

        public async Task<IEnumerable<MotivationPiece>> ListAsync(string userId)
        {
            var board = await _dbContext.MotivationBoards.GetAsync(userId);
            return await _dbContext.MotivationPieces
                .Where(p => p.MotivationBoardId == board.Id)
                .OrderBy(p => p.Order)
                .ToListAsync();
        }

        public async Task<MotivationPiece> CreateAsync(CreateMotivationPiece request)
        {
            var identityId = _identityResolver.GetId();
            var board = await _dbContext.MotivationBoards.GetAsync(identityId);

            var piece = request.Map();
            piece.MotivationBoardId = board.Id;
            piece.Order = await _dbContext.MotivationPieces.CountAsync(board.Id);
            _dbContext.MotivationPieces.Add(piece);

            return piece;
        }
    }

    public static class MotivationQueries
    {
        public static async Task<MotivationBoard> GetAsync(
            this IQueryable<MotivationBoard> queryable,
            string userId)
        {
            return await queryable.FirstAsync(b => b.UserId == userId);
        }

        public static async Task<int> CountAsync(
            this IQueryable<MotivationPiece> queryable,
            Guid boardId)
        {
            return await queryable.CountAsync(p => p.MotivationBoardId == boardId);
        }
    }
}
