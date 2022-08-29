using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Nest;

using Vulns.Core;

namespace Vulns.Infrastructure;
public class IssuerRepository : SearchableRepository<Issuer, IssuerDocument>
{
    public IssuerRepository(IElasticClient elastic, AppDbContext context, IMapper mapper) : base(elastic, context, mapper) { }

    public async Task<List<Issuer>> AllAsync(CancellationToken token)
        => await context.CveNumberingAuthority.ToListAsync(token);

    public override async Task<SearchResult<Issuer>> TypeaheadAsync(string? phrase = null, int size = 10, CancellationToken token = default)
    {
        var original = phrase;
        if (phrase == null) phrase = "*";
        else phrase = $"*{phrase}*";
        var matches = await elastic.SearchAsync<IssuerDocument>(s => s
            .Size(size)
            .Query(q => q
                .Bool(b => b
                    .MinimumShouldMatch(1)
                    .Should(
                        s => s.Prefix(p => p
                            .Field(i => i.ShortName)
                            .Boost(1.5)
                            .Value(original)
                            .CaseInsensitive()
                        ),
                        s => s.QueryString(qs => qs
                            .Query(phrase)
                            .Analyzer("standard")
                            .AutoGenerateSynonymsPhraseQuery()
                            .Fuzziness(Fuzziness.Auto)
                        )
                    )
                )
            )
        , token);
        var entities = mapper.Map<IEnumerable<Issuer>>(matches.Documents);
        return new(entities, matches.Total);
    }
}