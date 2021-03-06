﻿using Slim.Metadata;
using Slim.Mutation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Slim.Cache
{
    public class DatabaseCache
    {
        public DatabaseMetadata Database { get; set; }

        public List<TableCache> TableCaches { get; }

        public DatabaseCache(DatabaseMetadata database)
        {
            this.Database = database;

            this.TableCaches =  this.Database.Tables
                .Select(x => new TableCache(x))
                .ToList();
        }

        public void Apply(params StateChange[] changes)
        {
            foreach (var change in changes)
            {
                TableCaches.Single(x => x.Table == change.Table).Apply(change);
            }
        }
    }
}
