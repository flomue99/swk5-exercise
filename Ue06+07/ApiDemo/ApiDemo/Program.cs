var builder = WebApplication.CreateBuilder(args);

//controller service hinzufügen
builder.Services.AddControllers();

var app = builder.Build();

// variant 1
//app.MapGet("/time1", () => DateTime.UtcNow.ToString("o"));


// variant 2
//app.MapGet("/time1", () => Results.Content(content: DateTime.UtcNow.ToString("o"),
//                                           contentType: "text/plain",
//                                           statusCode: 200));


//variant 3
//app.MapGet("/time1", () => Results.Content(content: DateTime.UtcNow.ToString("o")));

//variant 4
//app.MapGet("/time1", () => Results.Json(data: new { Time = DateTime.UtcNow }));


//Sucht controller und klassen die von ControllerBase abgeleitet sind
app.MapControllers();

app.Run();
