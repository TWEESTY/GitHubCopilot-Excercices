using Copilot.Domain.Constants;
using Copilot.Infrastructure.Data;
using Copilot.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Copilot.Application.FunctionalTests;

[SetUpFixture]
public partial class Testing
{
    private static CustomWebApplicationFactory _factory = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static string? _userId;

    [OneTimeSetUp]
    public Task RunBeforeAnyTests()
    {
        _factory = new CustomWebApplicationFactory();

        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        return Task.CompletedTask;
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    [OneTimeTearDown]
    public async Task RunAfterAnyTests()
    {
        await _factory.DisposeAsync();
    }
}
