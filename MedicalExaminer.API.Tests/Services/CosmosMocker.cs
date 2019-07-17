﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Cosmonaut;
using MedicalExaminer.API.Tests.Helpers;
using MedicalExaminer.Common.ConnectionSettings;
using MedicalExaminer.Common.Database;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Moq;
using Newtonsoft.Json;
using Document = Microsoft.Azure.Documents.Document;

namespace MedicalExaminer.API.Tests.Services
{
    public static class CosmosMocker
    {
        public static Mock<IDocumentClientFactory> CreateClientFactory(Mock<IDocumentClient> client)
        {
            var clientFactory = new Mock<IDocumentClientFactory>();
            clientFactory.Setup(x => x.CreateClient(It.IsAny<IConnectionSettings>()))
                .Returns(client.Object);

            return clientFactory;
        }

        public static Mock<T> CreateConnectionSettings<T>()
            where T : class, IConnectionSettings
        {
            return new Mock<T>(
                new Mock<Uri>("https://anything.co.uk").Object, "a", "c");
        }

        public static Mock<ICosmosStore<T>> CreateCosmosStore<T>(T[] collectionDocuments)
            where T : class
        {
            var mock = new Mock<ICosmosStore<T>>();
            var provider = new Mock<IQueryProvider>();
            var mockOrderedQueryable = new Mock<IOrderedQueryable<T>>();
            var dataSource = collectionDocuments.AsQueryable();
            var mockDocumentQuery = new Mock<IFakeDocumentQuery<T>>();
            provider
                .Setup(_ => _.CreateQuery<T>(It.IsAny<Expression>()))
                .Returns((Expression expression) =>
                {
                    var query = new EnumerableQuery<T>(expression);
                    var response = new FeedResponse<T>(query);

                    mockDocumentQuery
                        .SetupSequence(_ => _.HasMoreResults)
                        .Returns(true)
                        .Returns(false);

                    mockDocumentQuery
                        .Setup(_ => _.ExecuteNextAsync<T>(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(response);

                    return mockDocumentQuery.Object;
                });
            mockOrderedQueryable.Setup(x => x.Provider).Returns(provider.Object);
            mockOrderedQueryable.Setup(x => x.Expression).Returns(() => dataSource.Expression);

            mock.Setup(x => x.Query(null)).Returns(mockOrderedQueryable.Object);

            return mock;
        }

        public static Mock<IDocumentClient> CreateDocumentClient<T>(T[] collectionDocuments)
            where T : class, new()
        {
            var client = new Mock<IDocumentClient>(MockBehavior.Strict);
            var mockOrderedQueryable = new Mock<IOrderedQueryable<T>>(MockBehavior.Strict);
            var provider = new Mock<IQueryProvider>(MockBehavior.Strict);
            var dataSource = collectionDocuments.AsQueryable();

            // Dynamic query for the "select" calls that filter by field names
            provider
                .Setup(_ => _.CreateQuery<object>(It.IsAny<Expression>()))
                .Returns((Expression expression) =>
                {
                    var mockDocumentQuery = new Mock<IFakeDocumentQuery<object>>(MockBehavior.Strict);
                    var query = new EnumerableQuery<object>(expression);
                    // This converts the anonymous types back to typed values.
                    var result = query.Select(item => GetItemFromDynamic<T>(item)).ToList();
                    var response = new FeedResponse<T>(result);

                    mockDocumentQuery.Setup(_ => _.Provider)
                        .Returns(provider.Object);
                    mockDocumentQuery.Setup(_ => _.Expression)
                        .Returns(expression);

                    mockDocumentQuery
                        .SetupSequence(_ => _.HasMoreResults)
                        .Returns(true)
                        .Returns(false);

                    mockDocumentQuery
                        .Setup(_ => _.ExecuteNextAsync<T>(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(response);

                    mockDocumentQuery
                        .Setup(_ => _.GetEnumerator())
                        .Returns(query.AsEnumerable().GetEnumerator());

                    return mockDocumentQuery.Object;
                });

            // Typed query for the initial "where clauses"
            provider
                .Setup(_ => _.CreateQuery<T>(It.IsAny<Expression>()))
                .Returns((Expression expression) =>
                {
                    var mockDocumentQuery = new Mock<IFakeDocumentQuery<T>>(MockBehavior.Strict);
                    var query = new EnumerableQuery<T>(expression);
                    var response = new FeedResponse<T>(query);

                    // Required for selects to pass to the above more generic version
                    mockDocumentQuery.Setup(_ => _.Provider)
                        .Returns(provider.Object);
                    mockDocumentQuery.Setup(_ => _.Expression)
                        .Returns(expression);

                    mockDocumentQuery
                        .SetupSequence(_ => _.HasMoreResults)
                        .Returns(true)
                        .Returns(false);

                    mockDocumentQuery
                        .Setup(_ => _.ExecuteNextAsync<T>(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(response);

                    mockDocumentQuery
                        .Setup(_ => _.GetEnumerator())
                        .Returns(query.AsEnumerable().GetEnumerator());

                    return mockDocumentQuery.Object;
                });

            mockOrderedQueryable.Setup(x => x.Provider).Returns(provider.Object);
            mockOrderedQueryable.Setup(x => x.Expression).Returns(() => dataSource.Expression);

            client.Setup(c => c.CreateDocumentQuery<T>(It.IsAny<Uri>(), It.IsAny<FeedOptions>()))
                .Returns(mockOrderedQueryable.Object);

            client.Setup(c => c.CreateDocumentAsync(
                    It.IsAny<Uri>(),
                    It.IsAny<T>(),
                    null,
                    false,
                    default(CancellationToken)))
                .Returns((Uri uri, T item, RequestOptions ro, bool b, CancellationToken ct) => GetDocumentForItem(item));

            client.Setup(c => c.CreateDocumentAsync(
                    It.IsAny<Uri>(),
                    It.IsAny<AuditEntry<T>>(),
                    null,
                    false,
                    default(CancellationToken)))
                .Returns((Uri uri, AuditEntry<T> item, RequestOptions ro, bool b, CancellationToken ct) => GetDocumentForItem(item));


            mockOrderedQueryable
                       .Setup(_ => _.GetEnumerator())
                       .Returns(dataSource.AsEnumerable().GetEnumerator());

            client.Setup(c => c.ReadDocumentAsync(It.IsAny<Uri>(),
                null,
                default(CancellationToken))).Returns((Uri item, RequestOptions ro, CancellationToken ct) => GetDocumentByIdForItem(item, mockOrderedQueryable.Object));

            client.Setup(c => c.UpsertDocumentAsync(
                    It.IsAny<Uri>(),
                    It.IsAny<T>(),
                    null,
                    false,
                    default(CancellationToken)))
                .Returns((Uri uri, T item, RequestOptions ro, bool b, CancellationToken ct) => GetDocumentForItem(item));

            return client;
        }

        private static Task<ResourceResponse<Document>> GetDocumentByIdForItem<T>(Uri uri, IOrderedQueryable<T> mockOrderedQueryable)
        {
            if (uri != null)
            {
                var id = uri.ToString().Split("/").Last();
                foreach (var item in mockOrderedQueryable)
                {
                    var itemAsDocument = GetDocumentForItem(item);
                    if (itemAsDocument.Result.Resource.Id == id)
                    {
                        return itemAsDocument;
                    }
                }
            }

            var error = new Error
            {
                Id = Guid.NewGuid().ToString(),
                Code = "404",
                Message = "Error Message"
            };

            throw CosmosExceptionThrowerHelper.CreateDocumentClientExceptionForTesting(error,
                                               HttpStatusCode.NotFound);
        }

        private static Task<ResourceResponse<Document>> GetDocumentForItem<T>(T item)
        {
            var document = new Document();

            // Attempt to provide something close to cosmos and pass the object through using its json property
            // names instead (to catch when ID is named differently)
            foreach (PropertyInfo propertyInfo in item.GetType().GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    var value = propertyInfo.GetValue(item, null);

                    var jsonProperty =
                        (JsonPropertyAttribute) propertyInfo.GetCustomAttribute(typeof(JsonPropertyAttribute));

                    document.SetPropertyValue(jsonProperty?.PropertyName ?? propertyInfo.Name, value);
                }
            }
            return Task.FromResult(new ResourceResponse<Document>(document));
        }

        /// <summary>
        /// Get Item from Dynamic.
        /// </summary>
        /// <remarks>Uses both the property names or the json names (json first) to query the dynamic object.</remarks>
        /// <typeparam name="T">The type of item you want back.</typeparam>
        /// <param name="raw">A dynamic object to query.</param>
        /// <returns>An typed object populated with fields from the dynamic object</returns>
        private static T GetItemFromDynamic<T>(dynamic raw)
            where T : class, new()
        {
            var result = new T();

            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                if (propertyInfo.CanWrite)
                {
                    Type type = raw.GetType();
                    PropertyInfo property = null;

                    var jsonProperty = (JsonPropertyAttribute)propertyInfo
                        .GetCustomAttribute(typeof(JsonPropertyAttribute));

                    // Try the json property string in the dunamic object first
                    if (jsonProperty != null)
                    {
                        property = type.GetProperties().FirstOrDefault(p => p.Name.Equals(jsonProperty.PropertyName));
                    }

                    //// Otherwise fall back to the c# property name.
                    //if (property == null)
                    //{
                    //    property = type.GetProperties().FirstOrDefault(p => p.Name.Equals(propertyInfo.Name));
                    //}

                    // Only if we found something should we set.
                    if (property != null)
                    {
                        var rawValue = property.GetValue(raw);
                        propertyInfo.SetValue(result, rawValue);
                    }
                }
            }

            return result;
        }
    }

    public interface IFakeDocumentQuery<T> : IDocumentQuery<T>, IOrderedQueryable<T>
    {
    }
}
