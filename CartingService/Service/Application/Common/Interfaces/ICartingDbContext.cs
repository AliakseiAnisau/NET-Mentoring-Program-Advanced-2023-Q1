﻿namespace CartingService.Application.Common.Interfaces;
public interface ICartingDbContext
{
    T Get<T>(string id);
    void Update<T>(T entity);
    void Insert<T>(T entity, CancellationToken cancellationToken = default);
    void Delete<T>(string id);
}
