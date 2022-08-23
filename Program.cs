using Autofac;
using Notification.Core.Interface;
using Notification.Core.Model;
using Notification.Core.Service;
using Notification.Model;
using Notification.Notifications;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<MemberWelcomeNotification>();
builder.Services.AddScoped<MemberLoginNotification>();
//builder.Services.BuildServiceProvider().GetRequiredService<IMemberService>();
//var n = new ContainerBuilder();
//n.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
//    .Where(w => w.Name.EndsWith("Service"))
//    .AsImplementedInterfaces();

//n.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
//    .Where(w => w.Name.EndsWith("Notification"))
//    .InstancePerLifetimeScope();
//builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("EmailSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
