using Ardalis.GuardClauses;
using AutoMapper;
using KaspelTestTask.Application.Features.OrderService.Models;
using KaspelTestTask.Application.Repositories.Abstractions;
using KaspelTestTask.Domain.Entities;
using MediatR;

namespace KaspelTestTask.Application.Features.OrderService.Queries;

public record GetFilteredOrdersQuery(Guid? OrderId, DateOnly? OrderDate) : IRequest<IEnumerable<OrderBrief>>
{
    public class Handler : IRequestHandler<GetFilteredOrdersQuery, IEnumerable<OrderBrief>>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;

        public Handler(IOrdersRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = Guard.Against.Null(ordersRepository);
            _mapper = Guard.Against.Null(mapper);
        }

        public async Task<IEnumerable<OrderBrief>> Handle(GetFilteredOrdersQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request, nameof(request));
            var orders = await _ordersRepository.GetFilteredOrdersAsync(request.OrderId, request.OrderDate, cancellationToken);
            var result = _mapper.Map<IEnumerable<OrderBrief>>(orders);
            return result;
        }
    }
}
