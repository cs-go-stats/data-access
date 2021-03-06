﻿using System;
using System.Linq.Expressions;
using CSGOStats.Infrastructure.DataAccess.Serialization;
using MongoDB.Bson.Serialization;
using NodaTime;

namespace CSGOStats.Infrastructure.DataAccess.Contexts.Mongo
{
    public static class MongoMappingExtensions
    {
        public static BsonMemberMap MapGuid<T>(
            this BsonClassMap<T> mapper, 
            Expression<Func<T, Guid>> guidPropertyExpression,
            bool isRequired = true)
                where T : class =>
                    mapper
                        .MapProperty(guidPropertyExpression)
                        .SetIsRequired(isRequired)
                        .SetSerializer(GuidMongoSerializer.Instance);

        public static BsonMemberMap MapOffsetDateTime<T>(
            this BsonClassMap<T> mapper,
            Expression<Func<T, OffsetDateTime>> datetimePropertyExpression,
            bool isRequired = true)
                where T : class =>
                    mapper
                        .MapProperty(datetimePropertyExpression)
                        .SetIsRequired(isRequired)
                        .SetSerializer(OffsetDateTimeMongoSerializer.Instance);

        public static BsonMemberMap MapOffsetDateTime<T>(
            this BsonClassMap<T> mapper,
            Expression<Func<T, OffsetDateTime?>> datetimePropertyExpression)
                where T : class =>
                    mapper
                        .MapProperty(datetimePropertyExpression)
                        .SetIgnoreIfNull(true)
                        .SetSerializer(NullableOffsetDateTimeMongoSerializer.Instance);

        public static BsonMemberMap MapRequired<TInstance, TProperty>(
            this BsonClassMap<TInstance> mapper,
            Expression<Func<TInstance, TProperty>> propertyExpression)
                where TInstance : class =>
                    mapper
                        .MapProperty(propertyExpression)
                        .SetIsRequired(true);

        public static BsonMemberMap MapOptional<TInstance, TProperty>(
            this BsonClassMap<TInstance> mapper,
            Expression<Func<TInstance, TProperty>> propertyExpression)
                where TInstance : class =>
                    mapper
                        .MapProperty(propertyExpression)
                        .SetIgnoreIfNull(true);
    }
}