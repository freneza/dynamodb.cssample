using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;

namespace dynamodb.sample.Business.Converter
{
    public abstract class DocumentConverter<T>
    {
        public abstract T ConvertToDomain(Dictionary<string, AttributeValue> item);
        public T ConvertToDomain(Document doc)
        {
            return ConvertToDomain(doc.ToAttributeMap());
        }
    }
}
