using System;
using FluentValidation;
using PageRank.Core.Features.Search.SearchUrlPositions;
using PageRank.Core.Models;

namespace PageRank.Infrastructure.Features.Search.SearchUrlPositions
{
    public class SearchUrlPositionsParametersValidator : AbstractValidator<SearchUrlPositionsParameters>
    {
        public SearchUrlPositionsParametersValidator()
        {
            RuleFor(x => x.Url).Must(u =>
            {
                try
                {
                    var temp = new Uri(u);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }).WithMessage("Url is invalid");

            RuleFor(x => x.SearchEngineType).Must(s =>
            {
                switch (s)
                {
                    case SearchEngineType.Google:
                    case SearchEngineType.Bing:
                        return true;
                    default:
                        return false;
                }
            }).WithMessage("SearchEngineType is invalid");
        }
    }
}