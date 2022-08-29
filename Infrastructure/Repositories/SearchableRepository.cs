using System.Linq.Expressions;
using System.Reflection;

using AutoMapper;

using Nest;

using Vulns.Core;

namespace Vulns.Infrastructure;
public abstract class SearchableRepository<TEntity, TDocument> : Repository<TEntity>, ISearchableRepository<TEntity>
    where TEntity : Entity
    where TDocument : class
{
    protected readonly IMapper mapper;

    protected SearchableRepository(IElasticClient elastic, AppDbContext context, IMapper mapper) : base(elastic, context)
        => this.mapper = mapper;

    protected virtual async Task IndexManyAsync(IEnumerable<TEntity> entities, CancellationToken token)
    {
        var documents = mapper.Map<IEnumerable<TDocument>>(entities);
        if (documents.Count() > 0)
            await elastic.IndexManyAsync<TDocument>(documents, cancellationToken: token);
    }

    protected virtual async Task ReIndexManyAsync(IEnumerable<TEntity> entities, CancellationToken token)
        => await IndexManyAsync(entities, token);

    public override async Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken token)
    {
        await base.AddManyAsync(entities, token);
        await IndexManyAsync(entities, token);
    }

    public override async Task UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken token)
    {
        await base.UpdateManyAsync(entities, token);
        await ReIndexManyAsync(entities, token);
    }

    public List<QueryContainer> CreateQueryContainer()
        => new List<QueryContainer>();

    public AggregationDictionary CreateAggregationContainer()
        => new();

    public SearchableRepository<TEntity, TDocument> AddDateRangeFilter(Expression<Func<TDocument, DateTime?>> field, DateTime? from, DateTime? to, List<QueryContainer> queryContainer)
    {
        var dateRangeFilter = new DateRangeQueryDescriptor<TDocument>().Field(field);
        var fieldHasBeenSet = false;
        if (from.HasValue && from.Value != default)
        {
            dateRangeFilter = dateRangeFilter.GreaterThanOrEquals(from);
            fieldHasBeenSet = true;
        }
        if (to.HasValue && to.Value != default)
        {
            dateRangeFilter = dateRangeFilter.LessThanOrEquals(to);
            fieldHasBeenSet = true;
        }
        if (fieldHasBeenSet)
            queryContainer.Add(new QueryContainerDescriptor<TDocument>().DateRange(_ => dateRangeFilter));
        return this;
    }

    public SearchableRepository<TEntity, TDocument> AddRangeFilter(Expression<Func<TDocument, double?>> field, double? from, double? to, List<QueryContainer> queryContainer)
    {
        var rangeFilter = new NumericRangeQueryDescriptor<TDocument>().Field(field);
        var fieldHasBeenSet = false;
        if (from.HasValue && from.Value != default)
        {
            rangeFilter = rangeFilter.GreaterThanOrEquals(from);
            fieldHasBeenSet = true;
        }
        if (to.HasValue && to.Value != default)
        {
            rangeFilter = rangeFilter.LessThanOrEquals(to);
            fieldHasBeenSet = true;
        }
        if (fieldHasBeenSet)
            queryContainer.Add(new QueryContainerDescriptor<TDocument>().Range(_ => rangeFilter));
        return this;
    }

    public SearchableRepository<TEntity, TDocument> AddSingleTermFilter(Expression<Func<TDocument, object?>> field, object? term, List<QueryContainer> queryContainer)
    {
        if (term != null)
            queryContainer.Add(new QueryContainerDescriptor<TDocument>().Term(field, term));
        return this;
    }

    public SearchableRepository<TEntity, TDocument> AddBoolFilters(IEnumerable<QueryContainer> queries, List<QueryContainer> queryContainer)
    {
        if (queries != null && queries.Any())
            queryContainer.Add(new QueryContainerDescriptor<TDocument>().Bool(b => b.Filter(queries.ToArray())));
        return this;
    }

    public SearchableRepository<TEntity, TDocument> AddBoolMinMatch(IEnumerable<QueryContainer> queries, List<QueryContainer> queryContainer, int minMatch = 1)
    {
        if (queries != null && queries.Any())
            queryContainer.Add(new QueryContainerDescriptor<TDocument>().Bool(b => b.Should(queries.ToArray()).MinimumShouldMatch(minMatch)));
        return this;
    }

    public SearchableRepository<TEntity, TDocument> AddMultiTermsFilter(Expression<Func<TDocument, object?>> field, IEnumerable<object>? terms, List<QueryContainer> queryContainer)
    {
        if (terms != null && terms.Any())
            queryContainer.Add(new QueryContainerDescriptor<TDocument>().Terms(t => t.Field(field).Terms(terms)));
        return this;
    }

    public SearchableRepository<TEntity, TDocument> AddNestedFilter(Expression<Func<TDocument, object?>> field, List<QueryContainer> queryContainer, params QueryContainer[] filters)
    {
        if (filters.Length > 0)
            queryContainer.Add(new QueryContainerDescriptor<TDocument>().Nested(q => q.Path(field).Query(q => q.Bool(b => b.Filter(filters)))));
        return this;
    }

    public SearchableRepository<TEntity, TDocument> AddTermPrefixFilter(Expression<Func<TDocument, object?>> field, object? prefix, List<QueryContainer> queryContainer, bool caseInsensitive = true)
    {
        var prefixFilter = new PrefixQueryDescriptor<TDocument>();
        prefixFilter.Field(field).Value(prefix).CaseInsensitive(caseInsensitive);
        if (prefix != null)
            queryContainer.Add(new QueryContainerDescriptor<TDocument>().Prefix(_ => prefixFilter));
        return this;
    }

    public virtual SearchDescriptor<TDocument> ConvertToSearchQuery(List<QueryContainer>? filters = null, int from = 0, int size = 10)
    {
        var boolQuery = new BoolQueryDescriptor<TDocument>();
        if (filters != null && filters.Any())
            boolQuery = boolQuery.Filter(filters.ToArray());

        return new SearchDescriptor<TDocument>()
            .From(from)
            .Size(size)
            .Query(q => q.Bool(_ => boolQuery));
    }

    public async Task<SearchResult<TEntity>> SearchAsync(SearchDescriptor<TDocument> searchQuery, CancellationToken token = default)
    {
        var matches = await elastic.SearchAsync<TDocument>(searchQuery, token);
        var entities = mapper.Map<IEnumerable<TEntity>>(matches.Documents);
        return new(entities, matches.Total);
    }

    public virtual async Task<SearchResult<TEntity>> TypeaheadAsync(string? phrase = null, int size = 10, CancellationToken token = default)
    {
        if (phrase == null) phrase = "*";
        else phrase = $"*{phrase}*";
        var matches = await elastic.SearchAsync<TDocument>(s => s
            .Size(size)
            .Query(q => q
                .QueryString(qs => qs
                    .Query(phrase)
                    .Analyzer("standard")
                    .AutoGenerateSynonymsPhraseQuery()
                    .Fuzziness(Fuzziness.Auto)
                )
            )
        , token);
        var entities = mapper.Map<IEnumerable<TEntity>>(matches.Documents);
        return new(entities, matches.Total);
    }

    public async Task<AggregateDictionary> AggregateAsync(AggregationDictionary aggs, QueryContainer? query = null, CancellationToken token = default)
    {
        var searchQuery = new SearchDescriptor<TDocument>().Size(0);
        if (query != null) searchQuery = searchQuery.Query(_ => query);
        if (aggs != null) searchQuery = searchQuery.Aggregations(aggs);
        var response = await elastic.SearchAsync<TDocument>(searchQuery);
        return response.Aggregations;
    }

    public DateHistogramAggregation CreateDateHistogramAggregation(string name, Expression<Func<TDocument, object?>> field, string interval = "")
    {
        interval = interval.ToLower().Trim();
        var aggInterval = DateInterval.Year;
        switch (interval)
        {
            case "year": aggInterval = DateInterval.Year; break;
            case "quarter": aggInterval = DateInterval.Quarter; break;
            case "month": aggInterval = DateInterval.Month; break;
            case "week": aggInterval = DateInterval.Week; break;
            case "day": aggInterval = DateInterval.Day; break;
            default: aggInterval = DateInterval.Year; break;
        }

        var aggregation = new DateHistogramAggregation(name)
        {
            CalendarInterval = aggInterval,
            Field = field,
        };
        return aggregation;
    }

    public TermsAggregation CreateTermsAggregation(string name, Expression<Func<TDocument, object?>> field, int size = 10, string orderKey = "_count", string orderDirection = "descending", AggregationDictionary? subaggregation = null)
    {

        // Consult the docs page, section: "Ordering by a sub aggregation"
        // Consult the docs page, section: "Filtering Values"
        var aggregation = new TermsAggregation(name);
        aggregation.Aggregations = subaggregation;
        aggregation.Size = size;
        aggregation.Field = field;
        aggregation.Order = new List<TermsOrder>(1) { new() { Key = orderKey, Order = orderDirection.ToLower().Contains("desc") ? SortOrder.Descending : SortOrder.Ascending } };
        var unaryExpr = field.Body as UnaryExpression;
        if (unaryExpr == null || unaryExpr.Operand.Type != typeof(Boolean))
            aggregation.Missing = "Unknown, missing, or not applicable";
        return aggregation;
    }

    public RangeAggregation CreateRangeAggregation(string name, Expression<Func<TDocument, object?>> field, AggregationDictionary? subaggregation = null)
    {

        var ranges = new List<AggregationRange>();
        for (int i = 0; i < 10; i++)
            ranges.Add(new AggregationRange() { From = i, To = i + 1, Key = $"{i}-{i + 1}" });

        return new RangeAggregation(name)
        {
            Ranges = ranges,
            Field = field,
            Aggregations = subaggregation
        };
    }

    public NestedAggregation CreateNestedAggregation(string name, Expression<Func<TDocument, object?>> field, int size = 10, AggregationDictionary? subaggregation = null)
        => new NestedAggregation(name) { Aggregations = subaggregation };

    public CardinalityAggregation CreateCardinalityAggregation(string name, Expression<Func<TDocument, object?>> field)
        => new(name, field) { PrecisionThreshold = 40_000 };
}