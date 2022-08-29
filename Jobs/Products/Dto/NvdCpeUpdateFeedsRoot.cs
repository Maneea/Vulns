using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace Vulns.Jobs.Products;
public class NvdCpeUpdateFeedsRoot
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
    public NvdCpeUpdates? NvdCpeData { get; set; }

    public bool HasMoreData() => ResultsPerPage + StartIndex < TotalResults;

    public class NvdCpeUpdates
    {
        [JsonProperty("cpes")]
        public List<Cpe>? Cpes { get; set; }
    }

    public class Cpe
    {
        [JsonProperty("cpe23Uri")]
        public string Cpe23Uri { get; set; } = string.Empty;

        [JsonProperty("lastModifiedDate")]
        public DateTime LastModifiedDate { get; set; }

        [JsonProperty("titles")]
        public List<Title> Titles { get; set; } = new List<Title>();

        [JsonProperty("refs")]
        public List<Ref> Refs { get; set; } = new List<Ref>();
    }

    public class Ref
    {
        [JsonProperty("ref")]
        public string Reference { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }

    public class Title
    {
        [JsonProperty("title")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("lang")]
        public string Lang { get; set; } = string.Empty;

        public bool IsEnglish() => Lang == "en_US";
    }
}