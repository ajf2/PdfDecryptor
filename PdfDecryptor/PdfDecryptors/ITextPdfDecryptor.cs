using iText.Kernel.Exceptions;
using iText.Kernel.Pdf;
using System.Text;

namespace PdfDecryptor.PdfDecryptors {
  public class ITextPdfDecryptor : IPdfDecryptor {
    public Task<bool> CheckIsEncryptedAsync(Parameters parameters) => Task.Run(() => {
      try {
        var pdfFile = new PdfDocument(new PdfReader(parameters.InputFilePath));
      } catch (BadPasswordException) {
        return true;
      }
      return false;
    });

    public Task DecryptAsync(Parameters parameters)
    {
      try
      {
        var readerProperties = new ReaderProperties().SetPassword(Encoding.UTF8.GetBytes(parameters.Password));
        var pdfReader = new PdfReader(parameters.InputFilePath, readerProperties);
        var pdfFile = new PdfDocument(
          pdfReader,
          new PdfWriter(parameters.OutputFilePath)
        );
        pdfFile.Close();
        return Task.CompletedTask;
      }
      catch (BadPasswordException)
      {
        throw new IncorrectPasswordException();
      }
    }
  }
}
