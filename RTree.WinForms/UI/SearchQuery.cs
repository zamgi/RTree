using System;
using System.Collections.Generic;

namespace trees.win_forms
{
    /// <summary>
    /// 
    /// </summary>
    public enum SearchByMethodEnum
    {
        Rect,
        Circle,
    }

    /// <summary>
    /// 
    /// </summary>
    internal sealed class/*struct*/ SearchQuery
    {
        public Envelope Envelope { get; init; }
        public SearchByMethodEnum SearchByMethod { get; init; }
        public IReadOnlyList< (RTreeRECT rc, float dist) > Results { get; init; }
        public int TopN { get; init; }
        public TimeSpan SearchElapsed { get; init; }

        public static implicit operator SearchQuery( 
            in (Envelope Envelope, SearchByMethodEnum SearchByMethod, IReadOnlyList< (RTreeRECT rc, float dist) > Results, int topN, TimeSpan SearchElapsed) t ) 
            => new SearchQuery() { Envelope = t.Envelope, SearchByMethod = t.SearchByMethod, Results = t.Results, TopN = t.topN, SearchElapsed = t.SearchElapsed };
    }
}
