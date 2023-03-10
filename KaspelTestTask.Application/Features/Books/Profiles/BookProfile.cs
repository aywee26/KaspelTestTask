using AutoMapper;
using KaspelTestTask.Application.Features.Books.Models;
using KaspelTestTask.Domain.Entities;

namespace KaspelTestTask.Application.Features.Books.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookBrief>()
            .ConstructUsing(b => new BookBrief(b.Id, b.Title, b.Author, b.PublicationDate, b.Price));
    }
}
