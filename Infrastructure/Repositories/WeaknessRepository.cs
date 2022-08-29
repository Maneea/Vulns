using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Nest;

using Vulns.Core;

namespace Vulns.Infrastructure;
public class WeaknessRepository : SearchableRepository<Weakness, WeaknessDocument>
{
    public WeaknessRepository(IElasticClient elastic, AppDbContext context, IMapper mapper) : base(elastic, context, mapper) { }

    public async override Task<Weakness?> GetAsync(string cweId, CancellationToken token)
        => await context.Weakness
            .Include(w => w.Platforms)
            .Include(w => w.Consequences)
            .Include(w => w.DetectionMethods)
            .Include(w => w.Children)
            .Include(w => w.Parent)
            .Where(_ => _.Id == cweId)
            .FirstOrDefaultAsync(token);

    public override async Task<SearchResult<Weakness>> TypeaheadAsync(string? phrase, int size, CancellationToken token)
    {
        var matches = await elastic.SearchAsync<WeaknessDocument>(s => s
            .Size(size)
            .Query(q => q.Bool(b => b
                .Should(
                    s => s.MatchBoolPrefix(mbp => mbp.Field(f => f.Name).Query(phrase)),
                    s => s.MatchBoolPrefix(mbp => mbp.Field(f => f.Id).Query($"CWE-{phrase}").Boost(0.5)),
                    s => s.MatchBoolPrefix(mbp => mbp.Field(f => f.Id).Query(phrase == null ? null : phrase.ToUpper())),
                    s => s.MatchBoolPrefix(mbp => mbp.Field(f => f.Id).Boost(1.5).Query(phrase)),
                    s => s.Term(w => w.Id, phrase == null ? null : phrase.ToUpper(), 10),
                    s => s.Term(w => w.Id, $"CWE-{phrase}", 10)
                )
                .MinimumShouldMatch(1)))
        , token);
        var entities = mapper.Map<IEnumerable<Weakness>>(matches.Documents);
        return new(entities, matches.Total);
    }

    public async Task<List<Weakness>> AllAsync(CancellationToken token)
        => await context.Weakness.Include(w => w.Platforms).ToListAsync(token);
}