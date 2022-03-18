namespace JsonEditor.Code
{
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
		public string Message { get; private set; }
		public string Schema { get; private set; }

		protected SchemaManagementResult(string schema, string message)
		{
			Schema = schema;
			Message = message;
		}
	}
}
