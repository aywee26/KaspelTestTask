using Ardalis.GuardClauses;
using KaspelTestTask.Application.Repositories.Abstractions;
using KaspelTestTask.Domain.Entities;
using MediatR;

namespace KaspelTestTask.Application.Features.OrderService.Queries;

public record GetAllOrdersQuery : IRequest<IEnumerable<Order>>
{
    public class Handler : IRequestHandler<GetAllOrdersQuery, IEnumerable<Order>>
    {
        private readonly IOrdersRepository _ordersRepository;

        public Handler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<IEnumerable<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request, nameof(request));
            return await _ordersRepository.GetAllOrdersAsync(cancellationToken);
        }
    }
}
