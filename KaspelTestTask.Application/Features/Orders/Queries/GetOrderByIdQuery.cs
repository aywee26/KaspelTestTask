using Ardalis.GuardClauses;
using AutoMapper;
using KaspelTestTask.Application.Features.Orders.Models;
using KaspelTestTask.Application.Repositories.Abstractions;
using MediatR;

namespace KaspelTestTask.Application.Features.Orders.Queries;

public record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto?>;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IOrdersRepository ordersRepository, IMapper mapper)
    {
        _ordersRepository = Guard.Against.Null(ordersRepository);
        _mapper = Guard.Against.Null(mapper);
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));
        var order = await _ordersRepository.GetOrderByIdAsync(request.Id, cancellationToken);
        var result = _mapper.Map<OrderDto>(order);
        return result;
    }
}
