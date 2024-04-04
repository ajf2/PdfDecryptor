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

    public Task<bool> DecryptAsync(Parameters parameters)
    {
      PdfReader? pdfReader = null;
      PdfWriter? pdfWriter = null;
      PdfDocument? pdfDocument = null;
      try
      {
        var readerProperties = new ReaderProperties().SetPassword(Encoding.UTF8.GetBytes(parameters.Password));
        pdfReader = new PdfReader(parameters.InputFilePath, readerProperties);
        pdfWriter = new PdfWriter(parameters.OutputFilePath);
        pdfDocument = new PdfDocument(pdfReader, pdfWriter);

        return Task.FromResult(true);
      }
      catch (BadPasswordException)
      {
        return Task.FromResult(false);
      }
      finally
      {
        pdfDocument?.Close();
        pdfReader?.Close();
        pdfWriter?.Close();
      }
    }
  }
}
