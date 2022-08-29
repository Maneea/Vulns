using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace Vulns.Jobs.Vulnerabilities;
public class NvdCveUpdateFeed
{
    [JsonProperty("resultsPerPage", Required = Required.Always)]
    [Required(AllowEmptyStrings = true)]
    public int ResultsPerPage { get; set; }

    [JsonProperty("startIndex", Required = Required.Always)]
    [Required(AllowEmptyStrings = true)]
    public int StartIndex { get; set; }

    [JsonProperty("totalResults", Required = Required.Always)]
    [Required(AllowEmptyStrings = true)]
    public int TotalResults { get; set; }

    [JsonProperty("result", Required = Required.Always)]
    [Required(AllowEmptyStrings = true)] 
    public NvdFeedRoot? NvdCveData { get; set; }

    public bool HasMoreData() => ResultsPerPage + StartIndex < TotalResults;
}