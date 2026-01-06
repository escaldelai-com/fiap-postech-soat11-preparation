using AutoMapper;
using MongoDB.Driver;
using Restaurant.Preparation.Application.DTO;
using Restaurant.Preparation.Application.Interfaces.Presenter;
using Restaurant.Preparation.Application.Interfaces.Repository;
using Restaurant.Preparation.Data.Model;

namespace Restaurant.Preparation.Data.Repositories;

public class OrderRepository(
    IMapper mapper,
    IDatePresenter presenter,
    IMongoDatabase context) : IOrderRepository
{

    private readonly IMongoCollection<OrderData> collection =
        context.GetCollection<OrderData>("order");

    public async Task<OrderDto?> Get(string? orderId)
    {
        var entity = await collection
            .Find(x => x.Id == orderId)
            .FirstOrDefaultAsync();

        if (entity == null)
            return null;

        entity.Data = presenter.ToTimeZone(entity.Data);

        return mapper.Map<OrderDto>(entity);
    }

    public async Task<IEnumerable<OrderDto>> GetListByStatuses(params string[] statuses)
    {
        var filter = Builders<OrderData>.Filter
            .In(x => x.Status, statuses);

        var entities = await collection
            .Find(filter)
            .ToListAsync();

        return mapper.Map<IEnumerable<OrderDto>>(entities);
    }

    public async Task<OrderDto> Create(OrderDto order)
    {
        var entity = mapper.Map<OrderData>(order);

        await collection.InsertOneAsync(entity);

        return mapper.Map<OrderDto>(entity);
    }

    public async Task<OrderDto> Update(OrderDto order)
    {
        var entity = mapper.Map<OrderData>(order);

        await collection.ReplaceOneAsync(
            x => x.Id == entity.Id, 
            entity);

        return mapper.Map<OrderDto>(entity);
    }

}
