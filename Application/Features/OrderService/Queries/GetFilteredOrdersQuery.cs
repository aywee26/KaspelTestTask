using Ardalis.GuardClauses;
using KaspelTestTask.Application.Repositories.Abstractions;
using KaspelTestTask.Domain.Entities;
using MediatR;

namespace KaspelTestTask.Application.Features.OrderService.Queries;

public record GetFilteredOrdersQuery(Guid? OrderId, DateOnly? OrderDate) : IRequest<IEnumerable<Order>>
{
    public class Handler : IRequestHandler<GetFilteredOrdersQuery, IEnumerable<Order>>
    {
        private readonly IOrdersRepository _ordersRepository;

        public Handler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public Task<IEnumerable<Order>> Handle(GetFilteredOrdersQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request, nameof(request));
            return _ordersRepository.GetFilteredOrdersAsync(request.OrderId, request.OrderDate, cancellationToken);
        }
    }
}
