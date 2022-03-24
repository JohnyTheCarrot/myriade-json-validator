using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JsonEditor.Code;

public static class TypeUtils
{
    public static IEnumerable<(string, JSchemaType)> GetPossibleTypes(JSchema? schema)
    {
        const string typeString = "Text";
        const string typeInteger = "Integer";
        const string typeFloat = "Decimal";
        const string typeBoolean = "Yes/No";
        const string typeArray = "List";
        const string typeObject = "Key/Value pair";
        const string typeNull = "No value";
            
        if (schema == null)
        {
            return new List<(string, JSchemaType)>
            {
                (typeString, JSchemaType.String),
                (typeInteger, JSchemaType.Integer),
                (typeFloat, JSchemaType.Number),
                (typeBoolean, JSchemaType.Boolean),
                (typeArray, JSchemaType.Array),
                (typeObject, JSchemaType.Object),
                (typeNull, JSchemaType.Null)
            };
        }
            
        var list = new List<(string, JSchemaType)>();
            
        if ((schema.Type & JSchemaType.String) != 0)
            list.Add((typeString, JSchemaType.String));

        if ((schema.Type & JSchemaType.Integer) != 0)
            list.Add((typeInteger, JSchemaType.Integer));

        if ((schema.Type & JSchemaType.Null) != 0)
            list.Add((typeNull, JSchemaType.Null));

        if ((schema.Type & JSchemaType.Object) != 0)
            list.Add((typeObject, JSchemaType.Object));

        if ((schema.Type & JSchemaType.Array) != 0)
            list.Add((typeArray, JSchemaType.Array));

        if ((schema.Type & JSchemaType.Number) != 0)
            list.Add((typeFloat, JSchemaType.Number));

        if ((schema.Type & JSchemaType.Boolean) != 0)
            list.Add((typeBoolean, JSchemaType.Boolean));

        return list;
    }
    
    public static JSchemaType JTokenTypeToJSchemaType(JTokenType type) => type switch
    {
        JTokenType.Array => JSchemaType.Array,
        JTokenType.Boolean => JSchemaType.Boolean,
        JTokenType.Float => JSchemaType.Number,
        JTokenType.Null => JSchemaType.Null,
        JTokenType.Object => JSchemaType.Object,
        JTokenType.Integer => JSchemaType.Integer,
        JTokenType.String => JSchemaType.String,
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };

    public static JToken GetDefaultValue(JTokenType type) => type switch
    {
        JTokenType.Object => new JObject(),
        JTokenType.Array => new JArray(),
        JTokenType.Integer => new JValue(0),
        JTokenType.Float => new JValue(0f),
        JTokenType.String => new JValue(string.Empty),
        JTokenType.Boolean => new JValue(false),
        JTokenType.Null => JValue.CreateNull(),
        _ => throw new ArgumentOutOfRangeException()
    };
    
    public static List<JTokenType> JSchemaTypeToJTokenType(JSchemaType type)
    {
        var list = new List<JTokenType>();
        
        if ((type & JSchemaType.String) != 0)
            list.Add(JTokenType.String);

        if ((type & JSchemaType.Integer) != 0)
            list.Add(JTokenType.Integer);

        if ((type & JSchemaType.Null) != 0)
            list.Add(JTokenType.Null);

        if ((type & JSchemaType.Object) != 0)
            list.Add(JTokenType.Object);

        if ((type & JSchemaType.Array) != 0)
            list.Add(JTokenType.Array);

        if ((type & JSchemaType.Number) != 0)
            list.Add(JTokenType.Float);

        if ((type & JSchemaType.Boolean) != 0)
            list.Add(JTokenType.Boolean);

        return list;
    }
}