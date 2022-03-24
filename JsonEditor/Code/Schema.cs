using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JsonEditor.Code;

public class Schema
{
	private const int MaxFileSizeBytes = 512_000;
	public const int MaxNumberOfFilesPerUpload = 20;
	private const string FileNameAllowedRegex = @"[\w\- \(\)\.]+";
	private static readonly Regex FileNameRenameRegex = new($@"^{FileNameAllowedRegex}$");
	private static readonly Regex FileNameRegex = new($@"^{FileNameAllowedRegex}\.json$");
	private const string SchemaFolderName = "schemas";

	private string FilePath { get; }
	public string FileName => Path.GetFileName(FilePath);

	private Schema(string filePath)
	{
		FilePath = filePath;
	}

	public static bool IsValidRenameFileName(string name)
	{
		return FileNameRenameRegex.IsMatch(name);
	}

	public static List<Schema> GetSchemas(string contentRootPath)
	{
		var basePath = Path.Combine(
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
		if (file.Size > MaxFileSizeBytes)
			return new SchemaManagementFailure(file.Name, $"Maximum filesize of {MaxFileSizeBytes / 1000}KB exceeded.");

		if (!FileNameRegex.IsMatch(file.Name))
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
		var jsonObjStr = Encoding.UTF8.GetString(jsonObj)
			.Replace($"{(char) 0xFEFF}", string.Empty); // Notepad adds a 0xFEFF zero width space at the start of files, which breaks parsing.

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

	private string GetContents() => File.ReadAllText(FilePath);

	public SchemaManagementResult Rename(string newName)
	{
		// TODO: log errors
		var basePath = Path.GetDirectoryName(FilePath)!;

		var path = Path.Combine(
			basePath,
			$"{newName}.json"
		);

		if (!FileNameRenameRegex.IsMatch(newName))
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