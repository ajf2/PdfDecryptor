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

    public Task DecryptAsync(Parameters parameters) => Task.Run(() => {
      var pdfFile = new PdfDocument(
          new PdfReader(parameters.InputFilePath, new ReaderProperties().SetPassword(Encoding.UTF8.GetBytes(parameters.Password))),
          new PdfWriter(parameters.OutputFilePath)
      );
      pdfFile.Close();
    });
  }
}
