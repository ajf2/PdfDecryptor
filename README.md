# PdfDecryptor

Simple command line app for decrypting password-protected PDF files, if you know the password. Built using iText, which does all the hard work better than the other packages I tried.

Usage:

```
PdfDecryptor.exe <paths-to-pdf-files>
```

Enter the correct password and it'll produce a `.dec.pdf` file for each input file, in the same location.

TODO?:
- Pressing backspace adds `\b` to the password string instead of removing the most recently added character.
- There may be problems with other keys e.g. Home/End/Cursors.
- It won't work if your PDF file's name doesn't end in `.pdf`.
- It probably throws an exception if your file is not a PDF file.
