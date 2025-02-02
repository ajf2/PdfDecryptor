string[] inputFilePaths = [];

var argsAreValid = ReadInputFilePath(args);
if (!argsAreValid)
  PrintUsage();
else {
  var passwords = new List<string>();
  foreach(var inputFilePath in inputFilePaths)
  {
    var fileHasBeenDecrypted = false;
    var parameters = new PdfDecryptor.Parameters(inputFilePath);
    var fileIsEncrypted = await PdfDecryptor.PdfDecryptor.CheckIsEncryptedAsync(parameters);
    
    if (fileIsEncrypted)
    {
      // Try entered passwords for all input files.
      foreach (var password in passwords)
      {
        parameters.Password = password;
        fileHasBeenDecrypted = await PdfDecryptor.PdfDecryptor.DecryptAsync(parameters);
        if (fileHasBeenDecrypted)
          break;
      }
      if (fileHasBeenDecrypted)
        continue;
      
      Console.Write("File is encrypted, enter password:");
      var keyInfo = Console.ReadKey(true);
      while (keyInfo.Key != ConsoleKey.Enter)
      {
        parameters.Password += keyInfo.KeyChar;
        keyInfo = Console.ReadKey(true);
      }

      fileHasBeenDecrypted = await PdfDecryptor.PdfDecryptor.DecryptAsync(parameters);
      while (!fileHasBeenDecrypted)
      {
        Console.Write($"{Environment.NewLine}Incorrect password, try again: ");
        parameters.Password = "";
        keyInfo = Console.ReadKey(true);
        while (keyInfo.Key != ConsoleKey.Enter)
        {
          parameters.Password += keyInfo.KeyChar;
          keyInfo = Console.ReadKey(true);
        }
        fileHasBeenDecrypted = await PdfDecryptor.PdfDecryptor.DecryptAsync(parameters);
      }
      passwords.Add(parameters.Password);
    }
    else
    {
      Console.WriteLine("File is not encrypted.");
    }
  }
}

return;

bool ReadInputFilePath(string[] args) {
  if (args.Length == 0)
    return false;
  if (args.Any(a => !File.Exists(a)))
    return false;
  inputFilePaths = args;
  return true;
}

void PrintUsage() => Console.WriteLine($"Usage:{Environment.NewLine}PdfDecryptor <paths-to-encrypted-pdfs>");
