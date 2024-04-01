namespace PdfDecryptor.PdfDecryptors {
  public interface IPdfDecryptor {
    public Task<bool> CheckIsEncryptedAsync(Parameters parameters);
    public Task DecryptAsync(Parameters parameters);
  }
}
