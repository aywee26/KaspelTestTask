using AutoMapper;
using KaspelTestTask.Application.Features.BookService.Models;
using KaspelTestTask.Domain.Entities;

namespace KaspelTestTask.Application.Features.BookService.Profiles;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<Book, BookBrief>()
            .ConstructUsing(b => new BookBrief(b.Id, b.Title, b.PublicationDate));
    }
}
