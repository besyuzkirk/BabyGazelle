using MediatR;

namespace BabyGazelle.BuildingBlocks.CQRS
{
    public interface IQueryHandler<in TQuery, TResponse>
     : IRequestHandler<TQuery, TResponse>
     where TQuery : IQuery<TResponse>
     where TResponse : notnull
    {
    }
}
