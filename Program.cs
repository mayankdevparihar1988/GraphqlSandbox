var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddGraphQLServer()
    .AddQueryType<QueryResolver>();



var app = builder.Build();

app.MapGraphQL();

app.MapGet("/", () => "Hello World!");

app.Run();
