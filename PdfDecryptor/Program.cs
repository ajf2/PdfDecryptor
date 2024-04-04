string inputFilePath = "";

var argsAreValid = ReadInputFilePath(args);
if (!argsAreValid)
  PrintUsage();
else {
  var parameters = new PdfDecryptor.Parameters(inputFilePath);
  var fileIsEncrypted = await PdfDecryptor.PdfDecryptor.CheckIsEncryptedAsync(parameters);
  if (fileIsEncrypted)
  {
    Console.Write("File is encrypted, enter password:");
    var keyInfo = Console.ReadKey(true);
    while (keyInfo.Key != ConsoleKey.Enter)
    {
      parameters.Password += keyInfo.KeyChar;
      keyInfo = Console.ReadKey(true);
    }

    var hasBeenDecrypted = await PdfDecryptor.PdfDecryptor.DecryptAsync(parameters);
    while (!hasBeenDecrypted)
    {
      Console.Write($"{Environment.NewLine}Incorrect password, try again: ");
      parameters.Password = "";
      keyInfo = Console.ReadKey(true);
      while (keyInfo.Key != ConsoleKey.Enter)
      {
        parameters.Password += keyInfo.KeyChar;
        keyInfo = Console.ReadKey(true);
      }
      hasBeenDecrypted = await PdfDecryptor.PdfDecryptor.DecryptAsync(parameters);
    }
  }
  else
  {
    Console.WriteLine("File is not encrypted.");
  }
}

bool ReadInputFilePath(string[] args) {
  if (!args.Any())
    return false;
  if (!File.Exists(args[0]))
    return false;
  inputFilePath = args[0];
  return true;
}

void PrintUsage() => Console.WriteLine($"Usage:{Environment.NewLine}PdfDecryptor <path-to-encrypted-pdf>");
