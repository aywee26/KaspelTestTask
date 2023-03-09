using AutoMapper;
using KaspelTestTask.Application.Features.OrderService.Models;
using KaspelTestTask.Domain.Entities;

namespace KaspelTestTask.Application.Features.OrderService.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderBrief>()
            .ConstructUsing(o => new OrderBrief(o.Id, o.OrderDate));
    }
}
