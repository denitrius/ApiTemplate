using ApiTemplate.Shared.Extensions;
using Asp.Versioning.ApiExplorer;

var app = WebApplication.CreateBuilder(args)
    .ConfigureBuilder() //Custom builder class
    .Build();

app.ConfigureApp(); //Custom configure app class

app.Run();
