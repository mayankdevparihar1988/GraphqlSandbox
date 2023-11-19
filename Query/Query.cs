

using System;
using System.Collections.Generic;
using System.Linq;
using HotChocolate.Types;



/**
 * 
 * 
 *  Pagination Logic with cursor 
 *  SCHEMA
 * 
 * */

public class Book
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
}

public class PageInfo
{
    public bool HasNextPage { get; set; }
    public string EndCursor { get; set; }
}

public class BookEdge
{
    public string Cursor { get; set; }
    public Book Node { get; set; }
}
public class BookConnection
{
    public List<BookEdge> Edges { get; set; }
    public PageInfo PageInfo { get; set; }
}



public class BookResponse
{
    public Book Data { get; set; }
    public string Metadata { get; set; }
    public Extension CountExtention { get; set; }
}



public class Extension
{
    public int Count { get; set; }

}



// QUERY


public class QueryResolver
{

    /**
    query {

        singleBook {
             data {
             id,
             title,
             author
          },
        metadata,
        countExtention {
                count
          }
       } 

    }
   */
    public BookResponse GetSingleBook()
    {

        // IMPLEMENT SERIVCE TO FETCH DATA
        var bookData = new Book
        {
            Id = "1",
            Title = "C# in depth.",
            Author = "Author 1"

        };

        var metadata = "Some metadata about the book.";

        var totalBookCount = 500;
        return new BookResponse
        {
            Data = bookData,
            Metadata = metadata,
            CountExtention = new Extension {
                Count = totalBookCount
            }
            
        };
    }

    public BookConnection GetBooks(int first, string after)
    {
        var books = allBooks;

        if (!string.IsNullOrEmpty(after))
        {
            var index = books.FindIndex(book => book.Id == after);
            if (index >= 0)
            {
                books = books.Skip(index + 1).ToList();
            }
        }

        var hasNextPage = books.Count > first;
        var endCursor = hasNextPage ? books[first - 1].Id : null;

        var pageInfo = new PageInfo { HasNextPage = hasNextPage, EndCursor = endCursor };

        var edges = books.Take(first).Select(book => new BookEdge { Cursor = book.Id, Node = book }).ToList();

        return new BookConnection { Edges = edges, PageInfo = pageInfo };
    }

    private static List<Book> GetBooks()
    {
        // Replace this with your actual data retrieval logic
        return new List<Book>
        {
            new Book { Id = "1", Title = "Book 1", Author = "Author 1" },
            new Book { Id = "2", Title = "Book 2", Author = "Author 2" },
            new Book { Id = "3", Title = "Book 3", Author = "Author 3" },
            new Book { Id = "4", Title = "Book 4", Author = "Author 4" },
            new Book { Id = "5", Title = "Book 5", Author = "Author 5" }
        };
    }
    private readonly List<Book> allBooks = GetBooks();



}
