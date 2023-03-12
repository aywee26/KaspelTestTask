using AutoMapper;
using KaspelTestTask.Application.Features.Orders.Models;
using KaspelTestTask.Domain.Entities;

namespace KaspelTestTask.Application.Features.OrderService.Profiles;

public class OrderedBookProfile : Profile
{
    public OrderedBookProfile()
    {
        CreateMap<OrderedBook, OrderedBookDto>()
            .ConstructUsing(ob => new OrderedBookDto(ob.Book.Id, ob.Book.Title, ob.Quantity, ob.Price));
    }
}
