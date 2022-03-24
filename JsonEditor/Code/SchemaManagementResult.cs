namespace JsonEditor.Code;

public class SchemaManagementSuccess : SchemaManagementResult
{
	public SchemaManagementSuccess(string schema) : base(schema, "Success!")
	{ }
}

public class SchemaManagementFailure : SchemaManagementResult
{
	public SchemaManagementFailure(string schema, string message) : base(schema, message)
	{ }
}

public class SchemaManagementResult
{
	public string Message { get; }
	public string Schema { get; }

	protected SchemaManagementResult(string schema, string message)
	{
		Schema = schema;
		Message = message;
	}
}