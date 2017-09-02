using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using CefSharp;
using CefSharp.OffScreen;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Chegg.Ebook.Autoprinter
{
    public class Program
    {
        private const string appVersion = "1.0.0";
        // The CefSharp Browser
        private static ChromiumWebBrowser browser;

        public static string eISBN = null, username = null, password = null;
        public static int pagesToPrint = -1;
        public static int printingSpeed = -1;

        public static void Main(string[] args)
        {
            try
            {
                const string testUrl = "https://www.chegg.com/auth?profile=VST&action=login";

                Console.Title = "Chegg E-book to PDF (Version: " + appVersion + ") by Jimmy Quach";
                WriteToConsole("Chegg E-book to PDF (Version: " + appVersion + ") by Jimmy Quach", ConsoleColor.White);
                WriteToConsole();
                WriteToConsole("The intended use of this program is to automate the printing (screenshoting) of every page from a selected e-book; a backup solution.", ConsoleColor.White);
                WriteToConsole();
                WriteToConsole("Disclaimer: Distributing copyrighted material without authorization is illegal in the United States and many other countries. I do not encourage or condone the illegal duplication or distribution of copyrighted content. Before copying or distributing any e-book content, make sure you have the legal right to do so.", ConsoleColor.Red);
                WriteToConsole();
                WriteToConsole();
                WriteToConsole();

                var settings = new CefSettings()
                {
                    // Remove this line to see chrome debug messages.
                    LogSeverity = LogSeverity.Error,
                    // By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data.
                    CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache")
                };

                // Perform dependency check to make sure all relevant resources are in our output directory.
                Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

                // Create the offscreen Chromium browser.
                browser = new ChromiumWebBrowser(testUrl);

                // The size of the browser, which also conveniently the page resolution. 
                // Can be adjusted for smaller files, but worser quality.
                //
                // TODO: Make this adjustable.
                //
                // 8.5 inches * 200 dpi = 1700 pixels
                // 11 inches * 200 dpi = 2200 pixels
                //
                // 8.5 inches * 144 dpi = 1224 pixels
                // 11 inches * 144 dpi = 1584 pixels
                //
                // 8.5 inches * 90 dpi = 765 pixels
                // 11 inches * 90 dpi = 990 pixels
                browser.Size = new Size(1224, 1584);

                // An event that is fired when the first page is finished loading.
                // This returns to us from another thread.
                browser.LoadingStateChanged += BrowserLoadingStateChanged;

                // We have to wait for something, otherwise the process will exit too soon
                while (browser != null) { }

                // Clean up Chromium objects.  You need to call this in your application otherwise
                // you will get a crash when closing.
                Cef.Shutdown();
                Environment.Exit(0);
            }
            catch (Exception err)
            {
                WriteToConsole("Error: Something went terribly wrong.", ConsoleColor.Red);
                WriteToConsole("Error: " + err, ConsoleColor.Red);
            }
        }

        private static void PreviewScreenshot()
        {
            // Give the browser a little time to render.
            Thread.Sleep(100);
            // Wait for the screenshot to be taken.
            var task = browser.ScreenshotAsync(true);
            task.ContinueWith(x =>
            {
                // Directory of file.
                var screenshotPath = Path.Combine(Environment.CurrentDirectory + "\\" + eISBN + "\\", "0000.jpeg");

                WriteToConsole();
                WriteToConsole("Preview Screenshot ready. Saving to " + screenshotPath);

                // Save the Bitmap to the path.
                // The image type is ".jpg".
                task.Result.Save(screenshotPath, ImageFormat.Jpeg);

                // We no longer need the Bitmap.
                // Dispose it to avoid keeping the memory alive.  Especially important in 32-bit applications.
                task.Result.Dispose();

                WriteToConsole("Preview Screenshot saved. Launching your default image viewer...");

                // Tell Windows to launch the saved image.
                Process.Start(screenshotPath);

                WriteToConsole("Image viewer launched.");

                bool promptAnswered = false;
                bool startPrint = false;
                while (!promptAnswered) // Loop indefinitely
                {
                    WriteToConsole();
                    WriteToConsole("If you see the first page of the selected e-book, then everything is correct.", ConsoleColor.Green);
                    WriteToConsole("If not, please check the e-ISBN again, and/or also make sure that you manually it set to the first page as Chegg remembers where you last left off.", ConsoleColor.Red);
                    WriteToConsole();
                    WriteToConsole("Would you like to start printing?. Confirm with 'Y' or 'N'", ConsoleColor.Yellow);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    string line = Console.ReadLine(); // Get string from user
                    if (line.ToLower() == "n") // Check string
                    {
                        WriteToConsole();
                        WriteToConsole("Program should be safe to close out now.", ConsoleColor.Green);
                        Cef.Shutdown();
                        Environment.Exit(0);
                        break;
                    }
                    else if (line.ToLower() == "y") // Check string
                    {
                        startPrint = true;
                        promptAnswered = true;
                        break;
                    }
                    else
                    {
                        WriteToConsole();
                        WriteToConsole("Confirm by typing 'Y' or 'N'", ConsoleColor.Yellow);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                }

                if (startPrint == true)
                {
                    WriteToConsole();
                    WriteToConsole("Printing setup...");
                    StartPrint();
                }
            }, TaskScheduler.Default);
        }

        public static void WriteToConsole(string line = "", ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(line);
        }

        private static void StartPrint()
        {
            while (printingSpeed == -1) // Loop indefinitely
            {
                WriteToConsole();
                WriteToConsole("Please set the screenshot delay, in milliseconds. (Recommended: '3000'. Any lower can create incorrect/missing pages.)", ConsoleColor.Yellow); // Prompt
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                string line = Console.ReadLine(); // Get string from user
                if (line == "exit") // Check string
                {
                    break;
                }
                printingSpeed = Convert.ToInt32(line);
            }

            while (pagesToPrint == -1) // Loop indefinitely
            {
                WriteToConsole();
                WriteToConsole("Please enter the last page of the book.", ConsoleColor.Yellow); // Prompt
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                string line = Console.ReadLine(); // Get string from user
                if (line == "exit") // Check string
                {
                    break;
                }
                pagesToPrint = Convert.ToInt32(line);
            }
            double eta = (((pagesToPrint * printingSpeed)/1000) * 0.0166667);
            WriteToConsole();
            WriteToConsole("Estimated time it takes to finish: " + Convert.ToInt32(eta) + " minutes.", ConsoleColor.Magenta);
            WriteToConsole("Printing Starting now...", ConsoleColor.Magenta);

            int count = 0;
            while (count != pagesToPrint)
            {
                // Locates and presses the next page button
                var scriptTask = browser.EvaluateScriptAsync("document.getElementsByClassName('navigation-button no-button horizontal-button next-button')[0].click()");
                // You need to pause for the page to render completely before taking a snapshot, internet speed might affect this, so its best to be adjusted
                Thread.Sleep(printingSpeed);
                eta = ((((pagesToPrint-count) * printingSpeed) / 1000) * 0.0166667);
                UpdateConsoleTitle(count, Convert.ToInt32(eta));
                scriptTask.ContinueWith(t =>
                {
                    // For some reason, It never removes the button from page 0001, but it removes it for later pages.
                    // TODO: Find out why that happens and fix it.
                    browser.EvaluateScriptAsync("document.getElementsByClassName('navigation-button no-button horizontal-button previous-button')[0].style.visibility = 'hidden'");
                    // Wait for the screenshot to be taken.
                    var task = browser.ScreenshotAsync();
                    task.ContinueWith(x =>
                    {
                        // Directory of file
                        // This will only rename files to 4 digits long. So theoretically speaking, 
                        // if the book has more than 9999 pages, this wouldn't work out. Lazy programming at its finest.
                        var screenshotPath = Path.Combine(Environment.CurrentDirectory + "\\" + eISBN + "\\", count.ToString("0000") + ".jpeg");

                        WriteToConsole();
                        WriteToConsole("Screenshot ready. Saving to " + screenshotPath);

                        // Save the Bitmap to the path.
                        // The image type is ".jpg".
                        task.Result.Save(screenshotPath, ImageFormat.Jpeg);

                        // We no longer need the Bitmap.
                        // Dispose it to avoid keeping the memory alive.  Especially important in 32-bit applications.
                        task.Result.Dispose();

                        WriteToConsole();
                        WriteToConsole("File " + count + " of " + pagesToPrint + " complete.", ConsoleColor.Magenta);
                    }, TaskScheduler.Default);
                });
                count++;
            }
            Thread.Sleep(printingSpeed);
            WriteToConsole();
            WriteToConsole("Printing complete!", ConsoleColor.Green);
            Console.Title = "Printing Complete!";

            WriteToConsole();
            WriteToConsole("Building .pdf setup...");
            BuildPDF();
        }

        private static void BuildPDF()
        {
            Console.Title = "Building .pdf...";
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "jpeg2pdf.exe")))
            {
                WriteToConsole();
                File.Copy(Path.Combine(Environment.CurrentDirectory, "jpeg2pdf.exe"), Path.Combine(Environment.CurrentDirectory + "\\" + eISBN + "\\", "jpeg2pdf.exe"), true);
                Thread.Sleep(2000);
                WriteToConsole("Copied '" + Path.Combine(Environment.CurrentDirectory, "jpeg2pdf.exe") + "' to '" + Path.Combine(Environment.CurrentDirectory + "\\" + eISBN + "\\", "jpeg2pdf.exe") + "'.");

                WriteToConsole("Building .pdf...");

                WriteToConsole("Finished Building .pdf! It is now safe to close out the program.", ConsoleColor.Green);
                WriteToConsole("Files are located in the directory: " + Path.Combine(Environment.CurrentDirectory + "\\" + eISBN + "\\"), ConsoleColor.Magenta);

                var startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var process = new Process { StartInfo = startInfo };

                process.Start();
                process.StandardInput.WriteLine("cd " + Environment.CurrentDirectory + "\\" + eISBN);
                process.StandardInput.WriteLine("jpeg2pdf -o " + eISBN + ".pdf -p auto -n portrait -z none -r height *.jpeg");
                process.StandardInput.WriteLine("exit");
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();
                process.Dispose();
                process.Kill();

                // For some reason, the process started above makes the program stay stuck even when killed, 
                // which never runs the lines below. Figure out why later.
                //
                //WriteToConsole(process.StandardOutput.ReadToEnd());
                //Thread.Sleep(5000);
                //WriteToConsole();
                //
                //if (File.Exists(Path.Combine(Environment.CurrentDirectory + "\\" + eISBN + "\\", eISBN + ".pdf")))
                //{
                //    WriteToConsole("Build complete! .pdf file created at '" + Path.Combine(Environment.CurrentDirectory + "\\" + eISBN + "\\", eISBN + ".pdf") + "'.", ConsoleColor.Green);
                //    Console.Title = "Build complete!";
                //
                //    WriteToConsole();
                //    WriteToConsole("Cleaning up temporary files...");
                //    // Delete the temporary files that were created in the process.
                //    File.Delete(Path.Combine(Environment.CurrentDirectory + "\\" + eISBN + "\\", "jpeg2pdf.exe"));
                //    Cef.Shutdown();
                //    Thread.Sleep(3000);
                //
                //    WriteToConsole("Clean up complete! Program is now safe to close.", ConsoleColor.Green);
                //}
                //else
                //{
                //    WriteToConsole("Error: Something went wrong building the .pdf", ConsoleColor.Red);
                //}
            }
            else
            {
                WriteToConsole();
                WriteToConsole("Error: Missing 'jpeg2pdf.exe' in directory.", ConsoleColor.Red);
            }
        }

        public static void UpdateConsoleTitle(int count, int minutesLeft)
        {
            Console.Title = "e-IBSN: " + eISBN +
                     "   |   Progress: " + count + "/" + pagesToPrint +
                     "   |   ETA: " + minutesLeft + " minutes left";
        }

        private static void BrowserLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            try {
                // Check to see if loading is complete - this event is called twice, one when loading starts
                // second time when it's finished
                // (rather than an iframe within the main frame).
                if (!e.IsLoading)
                {
                    WriteToConsole("Redirected to: " + browser.Address.ToString());

                    if (browser.Address.ToString() == "https://www.chegg.com/auth?profile=VST&action=login")
                    {
                        while (username == null) // Loop indefinitely
                        {
                            WriteToConsole();
                            WriteToConsole("Enter username:", ConsoleColor.Yellow); // Prompt
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            string line = Console.ReadLine(); // Get string from user
                            if (line == "exit") // Check string
                            {
                                break;
                            }
                            username = line;
                        }

                        browser.EvaluateScriptAsync("document.getElementsByName('email')[0].value = '" + username + "'");

                        while (password == null) // Loop indefinitely
                        {
                            WriteToConsole();
                            WriteToConsole("Enter password:", ConsoleColor.Yellow); // Prompt
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            string line = Console.ReadLine(); // Get string from user
                            if (line == "exit") // Check string
                            {
                                break;
                            }
                            password = line;
                        }

                        browser.EvaluateScriptAsync("document.getElementsByName('password')[0].value = '" + password + "'");

                        WriteToConsole();
                        WriteToConsole("Logging in...");

                        browser.EvaluateScriptAsync("document.getElementsByName('login')[0].click()");
                    }
                    else if (browser.Address.ToString() == "https://ereader.chegg.com/#/")
                    {
                        WriteToConsole();
                        WriteToConsole("Successfully logged in!", ConsoleColor.Green);
                        WriteToConsole("Do not log in from any other devices while this program is still running. It will log out this program out and cause problems.", ConsoleColor.Red);
                        WriteToConsole();
                        while (eISBN == null) // Loop indefinitely
                        {
                            WriteToConsole("Please enter the e-ISBN of the book that you wish to open:", ConsoleColor.Yellow); // Prompt
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            string line = Console.ReadLine(); // Get string from user
                            if (line == "exit") // Check string
                            {
                                break;
                            }
                            eISBN = line;
                        }
                        // Creates the directory to put all the pages in one neat folder
                        System.IO.Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + eISBN + "\\");
                        WriteToConsole();
                        WriteToConsole("Creating directory for pages: " + Environment.CurrentDirectory + "\\" + eISBN + "\\");

                        browser.Load("https://ereader.chegg.com/#/books/" + eISBN);
                        WriteToConsole("Checking book...");
                    }
                    else if (browser.Address.ToString().Contains("https://ereader.chegg.com/#/books/") && browser.Address.ToString().Contains("cfi"))
                    {
                        WriteToConsole();
                        WriteToConsole();
                        WriteToConsole("Loading Book...");
                        // Removes Chegg's ereader elements from the webpage.
                        Thread.Sleep(3000);
                        browser.EvaluateScriptAsync("document.getElementsByClassName('scrubber-tab noButton')[0].click()");
                        browser.EvaluateScriptAsync("document.getElementById('scrubber-container').style.visibility = 'hidden'");
                        browser.EvaluateScriptAsync("document.getElementsByClassName('navigation-button no-button horizontal-button next-button')[0].style.visibility = 'hidden'");
                        browser.EvaluateScriptAsync("document.getElementById('jigsaw-placeholder-outer').style.left = '22px'");
                        browser.EvaluateScriptAsync("document.getElementById('vertical-toolbar').style.visibility = 'hidden'");

                        // "Wait...
                        // Why are we doing screenshots instead of just ripping off the img src 
                        // from the img id 'pbk-page'? That's the entire page!!!"
                        //
                        // Well I tried, and I can't grab it for some reason; it just returns null.
                        // I don't know too much about javascript, so someone else can figure this out.
                        // So we're just going to take screenshots for now.
                        //
                        // document.getElementById('pbk-page').toDataURL(); ??????
                        //
                        Thread.Sleep(3000);
                        PreviewScreenshot();

                        WriteToConsole();
                        WriteToConsole();
                        WriteToConsole();
                        WriteToConsole();
                        WriteToConsole("If book never loads, try one of the following:");
                        WriteToConsole(">> Close out the program and try again.");
                        WriteToConsole(">> Log in through your web browser and open the book, then restart the program.");
                        WriteToConsole("Unknown issue, but usually fixes after some reattempts.");
                        WriteToConsole();
                        WriteToConsole();
                        WriteToConsole("Loading Book...");
                        browser.LoadingStateChanged -= BrowserLoadingStateChanged;
                    }
                }
                else
                {
                    WriteToConsole("You are being redirected...");
                }
            }
            catch (Exception err)
            {
                WriteToConsole("Error: Something went terribly wrong.", ConsoleColor.Red);
                WriteToConsole("Error: " + err, ConsoleColor.Red);
            }
        }
    }
}