using PdfDecryptor.PdfDecryptors;

namespace PdfDecryptor {
  public static class PdfDecryptor {
    public static async Task<bool> CheckIsEncryptedAsync(Parameters parameters) {
      try {
        return await new ITextPdfDecryptor().CheckIsEncryptedAsync(parameters);
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        return false;
      }
    }

    public static async Task<bool> DecryptAsync(Parameters parameters) =>
      await new ITextPdfDecryptor().DecryptAsync(parameters);
  }

  public class Parameters {
    public bool AreValid { get; set; }
    public string InputFilePath { get; set; } = "";
    public string OutputFilePath => InputFilePath.Replace(".pdf", ".dec.pdf");
    public string Password { get; set; } = "";

    public Parameters(string inputFilePath) {
      if (!inputFilePath.ToLower().EndsWith(".pdf"))
        return;
      if (!File.Exists(inputFilePath))
        return;

      InputFilePath = inputFilePath.Trim();
    }

    public Parameters(string inputFilePath, string password, string? outputFilePath = null) {
      if (!inputFilePath.ToLower().EndsWith(".pdf"))
        return;
      if (!File.Exists(inputFilePath))
        return;
      if (string.IsNullOrEmpty(password))
        return;
      if (!string.IsNullOrEmpty(outputFilePath) && !File.Exists(outputFilePath))
        return;

      InputFilePath = inputFilePath.Trim();
      Password = password;
      //OutputFilePath = string.IsNullOrEmpty(outputFilePath) ? inputFilePath.Replace(".pdf", ".dec.pdf") : outputFilePath;
    }
  }

  public class IncorrectPasswordException() : ApplicationException("Incorrect password");
}
