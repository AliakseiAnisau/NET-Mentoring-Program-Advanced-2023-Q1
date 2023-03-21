﻿using System.Globalization;
using CartingService.Application.TodoLists.Queries.ExportTodos;
using CsvHelper.Configuration;

namespace CartingService.Infrastructure.Files.Maps;
public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).Convert(c => c.Value.Done ? "Yes" : "No");
    }
}
