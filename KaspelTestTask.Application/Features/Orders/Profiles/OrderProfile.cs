using AutoMapper;
using KaspelTestTask.Application.Features.Orders.Models;
using KaspelTestTask.Domain.Entities;

namespace KaspelTestTask.Application.Features.OrderService.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>()
            .ConstructUsing(o =>
                new OrderDto(
                    o.Id,
                    o.OrderDate,
                    o.OrderedBooks.Select(ob => new OrderedBookDto(ob.BookId, ob.Book.Title, ob.Quantity, ob.Price))
             ));
    }
}
