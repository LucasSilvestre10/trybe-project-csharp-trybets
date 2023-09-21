using TryBets.Odds.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;

namespace TryBets.Odds.Repository;

public class OddRepository : IOddRepository
{
    protected readonly ITryBetsContext _context;
    public OddRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public Match Patch(int MatchId, int TeamId, string BetValue)
    {
        Match findedMatch = _context.Matches.FirstOrDefault(m => m.MatchId == MatchId)!;
        if (findedMatch == null) throw new Exception("Match not founded");
        Team findedTeam = _context.Teams.FirstOrDefault(t => t.TeamId == TeamId)!;
        if (findedTeam == null) throw new Exception("Team not founded");
        string newBetValue = BetValue.Replace(",", ".");
        if (findedMatch.MatchTeamAId != TeamId && findedMatch.MatchTeamBId != TeamId) throw new Exception("Team is not in this match");
        if (findedMatch.MatchTeamAId == TeamId) findedMatch.MatchTeamAValue += decimal.Parse(newBetValue, CultureInfo.InvariantCulture);
        else findedMatch.MatchTeamBValue += decimal.Parse(newBetValue, CultureInfo.InvariantCulture);

        _context.Matches.Update(findedMatch);
        _context.SaveChanges();

        return new Match
        {
            MatchId = MatchId,
            MatchDate = findedMatch.MatchDate,
            MatchTeamAId = findedMatch.MatchTeamAId,
            MatchTeamBId = findedMatch.MatchTeamBId,
            MatchTeamAValue = findedMatch.MatchTeamAValue,
            MatchTeamBValue = findedMatch.MatchTeamBValue,
            MatchFinished = findedMatch.MatchFinished,
            MatchWinnerId = findedMatch.MatchWinnerId,
            MatchTeamA = null,
            MatchTeamB = null,
            Bets = null,
        };
    }
}