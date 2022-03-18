using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JsonEditor.Code
{
	public class Schema
	{
		public const int maxFileSizeBytes = 512_000, maxNumberOfFilesPerUpload = 20;
		public const string fileNameAllowedRegex = @"[\w\- \(\)\.]+";
		public static Regex fileNameRenameRegex = new($@"^{fileNameAllowedRegex}$");
		public static Regex fileNameRegex = new($@"^{fileNameAllowedRegex}\.json$");
		private const string SchemaFolderName = "schemas";

		public string FilePath { get; private set; }
		public string FileName => Path.GetFileName(FilePath);

		public Schema(string filePath)
		{
			FilePath = filePath;
		}

		public static bool IsValidRenameFileName(string name)
        {
			return fileNameRenameRegex.IsMatch(name);
        }

		public static List<Schema> GetSchemas(string contentRootPath)
		{
			string basePath = Path.Combine(
				contentRootPath,
				SchemaFolderName
			);

			try
			{
				return Directory.GetFiles(basePath)
					.Select(f => new Schema(f))
					.ToList();
			} catch (DirectoryNotFoundException)
            {
				Directory.CreateDirectory(basePath);
				return GetSchemas(contentRootPath);
            }
		}

		public JSchema GetJSchema() => JSchema.Parse(GetContents());

		public static async Task<SchemaManagementResult> SaveSchema(
			string contentRootPath,
			IBrowserFile file
		) {
			if (file.Size > maxFileSizeBytes)
				return new SchemaManagementFailure(file.Name, $"Maximum filesize of {maxFileSizeBytes / 1000}KB exceeded.");

			if (!fileNameRegex.IsMatch(file.Name))
				return new SchemaManagementFailure(file.Name, "Invalid file name. Don't forget to end the file name with '.json'.");

			var path = Path.Combine(
				contentRootPath,
				SchemaFolderName,
				file.Name
			);

			if (File.Exists(path))
				return new SchemaManagementFailure(file.Name, "There is already a schema by that name.");

			var jsonObj = new byte[file.Size];
			await file.OpenReadStream().ReadAsync(jsonObj);
			var jsonObjStr = Encoding.UTF8.GetString(jsonObj).Replace($"{(char) 65279}", string.Empty);

			try
			{
				JToken.Parse(jsonObjStr);
			} catch (JsonReaderException ex)
			{
				return new SchemaManagementFailure(file.Name, "The JSON in this file is malformed. " + ex.Message);
			}

			await using FileStream fs = new(path, FileMode.Create);
			await file.OpenReadStream().CopyToAsync(fs);

			return new SchemaManagementSuccess(file.Name);
		}

		public string GetContents() => File.ReadAllText(FilePath);

		public SchemaManagementResult Rename(string newName)
		{
			// TODO: log errors
			var basePath = Path.GetDirectoryName(FilePath)!;

			var path = Path.Combine(
				basePath,
				$"{newName}.json"
			);

			if (!fileNameRenameRegex.IsMatch(newName))
				return new SchemaManagementFailure(FileName, "Invalid file name.");

			if (File.Exists(path))
				return new SchemaManagementFailure(FileName, "There is already a schema by that name.");

			try
			{
				File.Move(FilePath, path);
				return new SchemaManagementSuccess(FileName);
			}
			catch (FileNotFoundException) {
				return new SchemaManagementFailure(FileName, "The file did not exist at the time an attempt was made to rename it.");
			}
			catch (Exception)
			{
				return new SchemaManagementFailure(FileName, "An error occurred whilst trying to rename file.");
			}
		}

		public SchemaManagementResult Delete()
		{
			try
			{
				File.Delete(FilePath);
				return new SchemaManagementSuccess(FileName);
			} catch (Exception)
			{
				return new SchemaManagementFailure(FileName, "An error occurred whilst trying to delete file");
			}
		}
	}
}
