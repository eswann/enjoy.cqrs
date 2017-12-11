﻿namespace Cars.Projections
{
    public interface IProjection
    {
        string ProjectionId { get; }

        IProjectionMetadata Metadata { get; }
    }
}