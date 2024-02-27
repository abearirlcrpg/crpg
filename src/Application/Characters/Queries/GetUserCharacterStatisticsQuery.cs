using AutoMapper;
using Crpg.Application.Characters.Models;
using Crpg.Application.Common.Interfaces;
using Crpg.Application.Common.Mediator;
using Crpg.Application.Common.Results;
using Crpg.Domain.Entities.Servers;
using Microsoft.EntityFrameworkCore;

namespace Crpg.Application.Characters.Queries;

public record GetUserCharacterStatisticsQuery : IMediatorRequest<CharacterStatisticsViewModel>
{
    public int CharacterId { get; init; }
    public int UserId { get; init; }
    public GameMode GameMode { get; init; }

    internal class Handler : IMediatorRequestHandler<GetUserCharacterStatisticsQuery, CharacterStatisticsViewModel>
    {
        private readonly ICrpgDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ICrpgDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Result<CharacterStatisticsViewModel>> Handle(GetUserCharacterStatisticsQuery req, CancellationToken cancellationToken)
        {
            var character = await _db.Characters
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == req.CharacterId && c.UserId == req.UserId, cancellationToken);

            return character == null
                ? new(CommonErrors.CharacterNotFound(req.CharacterId, req.UserId))
                : new(_mapper.Map<CharacterStatisticsViewModel>(character.Statistics.FirstOrDefault(cs => cs.GameMode == req.GameMode) == null
                ? new CharacterStatisticsViewModel
                {
                    Kills = 0,
                    Deaths = 0,
                    Assists = 0,
                    PlayTime = TimeSpan.Zero,
                    GameMode = req.GameMode,
                    Rating = new CharacterRatingViewModel
                    {
                        CompetitiveValue = 0,
                        Value = 0,
                        Deviation = 0,
                    },
                }
                : character.Statistics.FirstOrDefault(cs => cs.GameMode == req.GameMode)));
        }
    }
}
