using System.ComponentModel.DataAnnotations;

namespace PageRank.Core.Features.Search.SearchUrlPositions
{
    public class SearchUrlPositionsParameters
    {
        [Required] public string Url { get; set; }

        [Required] [MaxLength(20)] public string SearchEngineType { get; set; }

        [Required] [MaxLength(50)] public string TitleSearch { get; set; }
    }
}