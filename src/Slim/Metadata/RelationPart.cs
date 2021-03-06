﻿namespace Slim.Metadata
{
    public enum RelationPartType
    {
        ForeignKey,
        CandidateKey
    }

    public class RelationPart
    {
        public Column Column { get; set; }
        public Relation Relation { get; set; }
        public RelationPartType Type { get; set; }
    }
}