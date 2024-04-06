namespace PdfDecryptor.PdfDecryptors {
  public interface IPdfDecryptor {
    public Task<bool> CheckIsEncryptedAsync(Parameters parameters);
    public Task<bool> DecryptAsync(Parameters parameters);
    public bool PasswordIsCorrect(FileStream readStream, string password);
  }
}
