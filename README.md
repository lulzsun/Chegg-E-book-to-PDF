# Chegg E-book to PDF
This program was created by Jimmy Quach (lulzsun), designed to simply download an e-book off of Chegg and convert it into a PDF.

[![Console Application Screenshot](http://i.imgur.com/IoUZt6K.png)](http://i.imgur.com/IoUZt6K.png)

# Donation
Donate to a college student who has no job and leeches off his parents.

[![Paypal](http://i.imgur.com/k53FXKP.gif)](https://www.paypal.me/jminquach)

# How to Use (For dummies)
0. Find your e-ISBN...
      1. Goto https://ereader.chegg.com/
      2. Click on the book that you want
      3. The e-ISBN is located in the URL of the webpage
          > Example: ht&#8203;tps://ereader.chegg.com/#/books/ ***e-ISBN*** /cfi/0!/4/2@100:0.00
      4. Write it down somewhere for later.
      
      Find the last page of the book...
      1. Goto https://ereader.chegg.com/
      2. Click on the book that you want
      3. Scroll all the way to the last page of the book
      4. The last page number is located in the URL of the webpage
          > Example: ht&#8203;tps://ereader.chegg.com/#/books/123456789/cfi/ ***Last page of the book*** !/4/2@100:0.00
      5. Write it down somewhere for later.
          
1. Download:
      * [Chegg E-book to PDF V1.0.1 for Windows 32-bit](https://mega.nz/#!cQZTTawD!eDybvAQo77LvpegHe10Kb_VYN0GBl1orLp7XRx5IzM8)
      * [Chegg E-book to PDF V1.0.1 for Windows 64-bit](https://mega.nz/#!VMR3hTQD!1SRtSzBL7azvIaH5yT3OMkZDKgeEOay5YE91qVFWcJI)
      
      Previous Version:
      * [Chegg E-book to PDF V1.0.0 for Windows 32-bit](https://mega.nz/#!UFBVFTbR!bEJvxqprOqc1i33ra9YRkDP01cCzvcAiOai2hdYClew)
      * [Chegg E-book to PDF V1.0.0 for Windows 64-bit](https://mega.nz/#!dE4hERrA!lmboUR538eI-aJsOPQGmOoO6bzB892XyolWXhDqAYrA)
      
2. Open up Chegg E-book to PDF.exe

3. Enter your Chegg login credentials.

4. Enter your e-ISBN value (found in step 0) of the book that you wish to open.
          
5. A preview screenshot of the book should pop-up. If you see the cover page of the selected book, then continue by entering the character 'Y'. If not, check the e-ISBN again and make sure that you manually set it to the first page as Chegg remembers where you last left off.

6. Set the screenshot delay, in milliseconds (Recommended: '3000').

7. Enter the last page value (found in step 0) of the book.
          
8. Wait for all the pages to be printed and built into a PDF.

9. After program claims its complete, close the program and the PDF will be located in the directory named after the e-ISBN.

# Bugs / Issues
* Program sometimes does not close out the process completely, which eats up the memory. If this happens, end the process with task manager.
* "I'm getting this error:"
     > Unhandled Exception: System.IO.FileNotFoundException: Could not load file or assembly 'CefSharp.Core.dll' or one of its dependencies. The specified module could not be found. at Chegg.Ebook.Autoprinter.Program.Main(String[] args)
     
     i. [VC++ 2012/2013 Redistributable Package](https://www.microsoft.com/en-us/download/details.aspx?id=40784) is required in order to run CefSharp on non developer machines. Forgot to include the .dlls that are required; will do that next version.
* Sometimes the preview does not remove Chegg's ereader browser elements, if you notice this, you can restart and try again to see if that fixes it.
