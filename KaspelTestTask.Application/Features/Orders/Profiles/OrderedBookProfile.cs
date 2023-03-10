using AutoMapper;
using KaspelTestTask.Application.Features.Orders.Models;
using KaspelTestTask.Domain.Entities;

namespace KaspelTestTask.Application.Features.OrderService.Profiles;

public class OrderedBookProfile : Profile
{
    public OrderedBookProfile()
    {
        CreateMap<OrderedBook, OrderedBookBrief>()
            .ConstructUsing(ob => new OrderedBookBrief(ob.BookId, ob.Quantity, ob.Price));
    }
}
