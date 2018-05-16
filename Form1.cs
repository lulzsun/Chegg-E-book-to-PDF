using System;
using System.Windows.Forms;
using CefSharp;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Chegg_E_book_to_PDF
{
    public partial class Form1 : Form
    {
        DateTime jobStartTime;
        public int[] delay = new int[] { 250, 3000 };
        public double qualityMultiplier = 2.0;
        public int jobCurrBookPage = -1, jobAliveCheck = -1, jobLastBookPage = -1;
        public string jobBookTitle = null, jobBookAuthor = null, jobBookEISBN = null;

        private bool debug = false;
        private const string loginURL = "https://www.chegg.com/auth?profile=VST&action=login";
        private static CefSharp.WinForms.ChromiumWebBrowser browser;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DebugMode(debug);

            var settings = new CefSettings()
            {
                // Remove this line to see chrome debug messages.
                LogSeverity = LogSeverity.Error,
                // By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data.
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache")
            };
            
            // Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

            //Init Browser Control View
            browser = new CefSharp.WinForms.ChromiumWebBrowser(loginURL)
            {
                Dock = DockStyle.Fill,
                TabStop = false
            };
            this.debugBrowser.Controls.Add(browser);

            // An event that is fired when the first page is finished loading.
            // This returns to us from another thread.
            browser.LoadingStateChanged += BrowserLoadingStateChanged;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (jobCurrBookPage != -1)
            {
                DialogResult dialogResult = MessageBox.Show("You are currently downloading a book. Are you sure you want to exit?", "Download In Progress", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Cef.Shutdown();
                }
            }
            else
            {
                Cef.Shutdown();
            }
        }

        private async void BrowserLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            try
            {
                // Check to see if loading is complete - this event is called twice, one when loading starts
                // second time when it's finished
                // (rather than an iframe within the main frame).
                if (!e.IsLoading)
                {
                    WriteToLogs("Redirected to: " + browser.Address.ToString());

                    if (browser.Address.ToString() == loginURL)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.emailTextbox.Enabled = true;
                            this.passwordTextbox.Enabled = true;
                            this.loginButton.Enabled = true;
                            this.fetchBooksButton.Enabled = false;
                            this.loadingIcon.Visible = false;
                        });

                        WriteToLogs("Ready to login.");
                    }
                    else if (browser.Address.ToString() == "https://ereader.chegg.com/#/")
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.emailTextbox.Enabled = false;
                            this.passwordTextbox.Enabled = false;
                            this.loginButton.Enabled = false;
                            this.fetchBooksButton.Enabled = true;
                            this.loadingIcon.Visible = false;
                        });

                        WriteToLogs("Successfully logged in.");
                        WriteToLogs("Attempting to retrieve books...");

                        await Task.Delay(delay[1]);
                        GetBooks();
                    }
                    else if (browser.Address.ToString().Contains("https://ereader.chegg.com/#/books/") && browser.Address.ToString().Contains("cfi/" + jobCurrBookPage + "!"))
                    {
                        browser.LoadingStateChanged -= BrowserLoadingStateChanged;
                        if (jobCurrBookPage == 0)
                        {
                            WriteToLogs("Successfully loaded book.");
                            WriteToLogs("Preparing book for printing...");
                        }

                        await Task.Delay(delay[0]);
                        browser.LoadingStateChanged += BrowserLoadingStateChanged;
                        if (jobCurrBookPage == 1)
                        {
                            await Task.Delay(delay[1]);
                            //Calculates the last page of book
                            FindLastPage();
                        }
                        //We need to access the origin of the page to get the source of the page image. 
                        //Conveniently, we just need to replace ereader with jigsaw and We should have access.
                        browser.Load(browser.Address.ToString().Replace("https://ereader.chegg.com/#/books/", "https://jigsaw.chegg.com/books/"));
                    }
                    else if (browser.Address.ToString().Contains("https://jigsaw.chegg.com/books/") && browser.Address.ToString().Contains("cfi=/" + jobCurrBookPage + "!"))
                    {
                        browser.LoadingStateChanged -= BrowserLoadingStateChanged;
                        WriteToLogs("Awaiting image download...");

                        await Task.Delay(delay[0]);
                        browser.LoadingStateChanged += BrowserLoadingStateChanged;
                        DownloadPage();
                    }
                    else if (browser.Address.ToString().Contains("chegg.com/signout") || browser.Address.ToString().Contains("jigsaw.chegg.com/login"))
                    {
                        WriteToLogs("Error: Signed out of Chegg");
                        MessageBox.Show("You've logged in from another browser! The program will close out now.", "Error: Signed out of Chegg", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Cef.Shutdown();
                        Environment.Exit(0);
                    }
                }
                else
                {
                    if (jobCurrBookPage != -1) jobAliveCheck = 1;
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.loadingIcon.Visible = true;
                    });
                    WriteToLogs("You are being redirected...");
                }
            }
            catch (Exception err)
            {
                WriteToLogs("Error: Something went terribly wrong.");
                WriteToLogs("Error: " + err);

                MessageBox.Show("Error: " + err, "Error: Something went terribly wrong.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GetBooks()
        {
            var task = browser.EvaluateScriptAsync(
                @"
                (function() {
                    var arr = [], l = document.getElementsByTagName('a');
                    for(var i=0; i<l.length; i++) {
                        if(l[i].href.indexOf('ereader.chegg.com/#/books/') > -1) {
                            arr.push(l[i].href + '$$$' + l[i].getElementsByTagName('img')[0].src + l[i].innerText.split('\n').join('$$$') + '@@@');
                        }
                    }
                    return arr.toString();
                 })()"
            );

            task.ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    var response = t.Result;
                    if (response.Success && response.Result != null && (string)response.Result != "")
                    {
                        this.Invoke((MethodInvoker)delegate { this.booksListView.Items.Clear(); });

                        string[] books = response.Result.ToString().Split(new[] { "@@@," }, StringSplitOptions.None);

                        ImageList images = new ImageList();
                        images.ImageSize = new Size(100, 122);
                        int listIndex = 0;

                        foreach (string book in books)
                        {
                            string[] bookDetails = book.Split(new[] { "$$$" }, StringSplitOptions.None);

                            WriteToLogs(book);

                            this.Invoke((MethodInvoker)delegate
                            {
                                images.Images.Add(LoadImage(bookDetails[1]));
                                this.booksListView.LargeImageList = images;
                                this.booksListView.Items.Add(
                                    bookDetails[2] + "\n" +                                                         //Title of Book
                                    bookDetails[3].Split(new[] { "ISBN: " }, StringSplitOptions.None)[0] + "\n" +   //Author of Book
                                    bookDetails[3].Split(new[] { "ISBN: " }, StringSplitOptions.None)[1],           //eISBN of Book
                                    listIndex);
                            });
                            listIndex++;
                        }
                    }
                }
            }, TaskScheduler.Default);

            WriteToLogs("Successfully retrieved books.");
        }

        public void FindLastPage()
        {
            //Grabs the width of the page scrubber, using the value to calculate the last page.
            var task = browser.EvaluateScriptAsync("document.getElementById('progress').style.width");

            task.ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    var response = t.Result;
                    if (response.Success && response.Result != null && (string)response.Result != "")
                    {
                        double n1 = double.Parse(response.Result.ToString().Remove(response.Result.ToString().Length - 1));           //Get first term (second term) of arithmetic sequence
                        double lastPageAsDouble = ((100 - n1) / n1) + 1;                                                              //using the arithmetic sequence formula to find last page
                        int lastPage = int.Parse(lastPageAsDouble.ToString().Substring(0, lastPageAsDouble.ToString().IndexOf('.'))); //Cut off repeating numbers
                        jobLastBookPage = lastPage;

                        WriteToLogs("Found last page of book: " + jobLastBookPage);
                    }
                }
            });
        }

        public void DownloadPage()
        {
            //'pdk-page' contains the image source of the page, we can download it by
            //using another javascript that initates the download of the image.
            var task = browser.EvaluateScriptAsync("document.getElementById('pbk-page').src");

            task.ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    var response = t.Result;
                    if (response.Success && response.Result != null && (string)response.Result != "")
                    {
                        //Gets the URL of the book page
                        string downloadUrl = response.Result.ToString();
                        int imageWidth = 2000;
                        try{
                            imageWidth = int.Parse(downloadUrl.Substring(downloadUrl.IndexOf("/encrypted/")).Replace("/encrypted/", ""));
                        } catch(FormatException e) { // if the above isn't parsable to int (ie it's something like "undefined")
                            imageWidth = 2000; // I feel like this should be an entry under the "qualityMultiplier". That or just keep it a standard number for all of the sheets. Is it a space-saving measure?
                        } finally {
                            downloadUrl = downloadUrl.Split(new[] { "encrypted/" }, StringSplitOptions.None)[0] + "encrypted/" + (imageWidth * qualityMultiplier);
                        }
                        
                        WriteToLogs("Downloading img src: " + downloadUrl);

                        //Setup path to download
                        string screenshotPath = Path.Combine(Environment.CurrentDirectory + "\\" + jobBookEISBN + "\\", jobCurrBookPage.ToString("0000") + ".jpeg");
                        browser.DownloadHandler = new DownloadHandler(screenshotPath);

                        //Initiate Download of image
                        browser.EvaluateScriptAsync(
                            // Source: http://pixelscommander.com/en/javascript/javascript-file-download-ignore-content-type/
                            "(function() { sUrl = '" + downloadUrl + @"';
                                //Creating new link node.
                                var link = document.createElement('a');
                                link.href = sUrl;
                                link.setAttribute('target','_blank');

                                if (link.download !== undefined) {
                                    //Set HTML5 download attribute. This will prevent file from opening if supported.
                                    var fileName = sUrl.substring(sUrl.lastIndexOf('/') + 1, sUrl.length);
                                    link.download = fileName;
                                }

                                //Dispatching click event.
                                if (document.createEvent) {
                                    var e = document.createEvent('MouseEvents');
                                    e.initEvent('click', true, true);
                                    link.dispatchEvent(e);
                                    return true;
                                }

                                // Force file download (whether supported by server).
                                if (sUrl.indexOf('?') === -1) {
                                    sUrl += '?download';
                                }

                                window.open(sUrl, '_blank');
                                return true;
                            })()");

                        WriteToLogs("File " + jobCurrBookPage + " of " + jobLastBookPage + " complete.");

                        //Continue to the next page
                        jobCurrBookPage++;
                        if (jobCurrBookPage > jobLastBookPage && jobLastBookPage != -1) //if its the last page
                        {
                            jobAliveCheck = -1;
                            WriteToLogs("All pages have been downloaded!");
                            BuildPDF();
                        }
                        else
                        {
                            browser.Load("https://ereader.chegg.com/#/books/" + jobBookEISBN + "/cfi/" + jobCurrBookPage + "!/4/2@100:0.00");
                            UpdateProgress();
                        }
                    }
                }
            });
        }

        public void UpdateProgress()
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.progressLabel.Visible = true;
                this.progressBar.Visible = true;

                if (jobLastBookPage != -1)
                {
                    TimeSpan timespent = DateTime.Now - jobStartTime;
                    int secondsremaining = (int)(timespent.TotalSeconds / jobCurrBookPage * (jobLastBookPage - jobCurrBookPage));

                    this.progressLabel.Text = "Progress: " + jobCurrBookPage + "/" + jobLastBookPage + "\r\n" +
                                              "Time Remain: " + (secondsremaining / 60) + "m" + (secondsremaining % 60) + "s";
                    this.progressBar.Maximum = jobLastBookPage;
                    this.progressBar.Value = jobCurrBookPage;
                }
            });
        }

        public void BuildPDF()
        {
            WriteToLogs("Building PDF file...");
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "jpeg2pdf.exe")))
            {

                File.Copy(Path.Combine(Environment.CurrentDirectory, "jpeg2pdf.exe"), Path.Combine(Environment.CurrentDirectory + "\\" + jobBookEISBN + "\\", "jpeg2pdf.exe"), true);
                System.Threading.Thread.Sleep(delay[1]);
                WriteToLogs("Copied '" + Path.Combine(Environment.CurrentDirectory, "jpeg2pdf.exe") + "' to '" + Path.Combine(Environment.CurrentDirectory + "\\" + jobBookEISBN + "\\", "jpeg2pdf.exe") + "'.");
                WriteToLogs("Building .pdf...");
                WriteToLogs("Finished Building .pdf!");
                WriteToLogs("PDF is located at " + Path.Combine(Environment.CurrentDirectory + "\\" + jobBookEISBN + "\\", jobBookEISBN + ".pdf"));

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
                process.StandardInput.WriteLine("cd " + Environment.CurrentDirectory + "\\" + jobBookEISBN);
                process.StandardInput.WriteLine("jpeg2pdf -o " + jobBookEISBN + ".pdf -p auto -n portrait -z none -r height *.jpeg");
                process.StandardInput.Flush();
                process.StandardInput.Close();

                while (!process.StandardOutput.EndOfStream)
                {
                    WriteToLogs(process.StandardOutput.ReadLine());
                }
                process.Close();
                process.Dispose();

                WriteToLogs("PDF file built!");
                MessageBox.Show("Finished converting book to PDF!\r\nPDF is located at " + Path.Combine(Environment.CurrentDirectory + "\\" + jobBookEISBN + "\\", jobBookEISBN + ".pdf"), "Download Complete!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Missing 'jpeg2pdf.exe' in directory.\r\nCould not build PDF.", "Error: Missing 'jpeg2pdf.exe' in directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                WriteToLogs("Error: Missing 'jpeg2pdf.exe' in directory.");
            }
            JobCleanUp();
        }

        public void JobCleanUp()
        {
            WriteToLogs("Cleaning up job.");

            jobCurrBookPage = -1; jobLastBookPage = -1;
            jobBookTitle = null; jobBookAuthor = null; jobBookEISBN = null;

            this.Invoke((MethodInvoker)delegate
            {
                this.fetchBooksButton.Enabled = true;
                this.downloadButton.Enabled = true;
                this.progressLabel.Text = "Progress: Calculating..." + "\r\n" +
                                          "Time Remain: Calculating...";
                this.progressBar.Value = 0;
                this.progressLabel.Visible = false;
                this.progressBar.Visible = false;
            });
            WriteToLogs("Finished cleaning.");
            WriteToLogs("Ready for next job.");
            browser.Load("https://ereader.chegg.com/#/");
        }

        public void DebugMode(bool enabled)
        {
            if (enabled)
            {
                tabControl1.ItemSize = new Size(50, 18);
                tabControl1.Appearance = TabAppearance.Normal;
            }
            else
            {
                tabControl1.ItemSize = new Size(0, 1);
                tabControl1.Appearance = TabAppearance.FlatButtons;
            }
        }

        public Image LoadImage(string url)
        {
            System.Net.WebRequest request =
                System.Net.WebRequest.Create(url);

            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream =
                response.GetResponseStream();

            Bitmap bmp = new Bitmap(responseStream);

            responseStream.Dispose();

            return bmp;
        }

        public void WriteToLogs(string line = "")
        {
            logsTextbox.Invoke(new Action(() => logsTextbox.AppendText("[" + DateTime.Now.ToString("g") + "]   " + line + "\r\n")));
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            browser.EvaluateScriptAsync("document.getElementsByName('email')[0].value = '" + emailTextbox.Text + "'");
            browser.EvaluateScriptAsync("document.getElementsByName('password')[0].value = '" + passwordTextbox.Text + "'");
            browser.EvaluateScriptAsync("document.getElementsByName('login')[0].click()");

            WriteToLogs("Attempting to login...");
        }

        private void applySettingsButton_Click(object sender, EventArgs e)
        {
            DebugMode(debugCheckBox.Checked);

            delay[0] = int.Parse(delay0Textbox.Text);
            delay[1] = int.Parse(delay1Textbox.Text);
            hangChecker.Interval = int.Parse(aliveCheckTextbox.Text);
            qualityMultiplier = double.Parse(qualityTextbox.Text);
        }

        private void revertSettingsButton_Click(object sender, EventArgs e)
        {
            DebugMode(false);
            debugCheckBox.Checked = false;
            delay[0] = 250;
            delay[1] = 3000;
            hangChecker.Interval = 30000;
            qualityMultiplier = 2.0;
        }

        private void exitSettingsButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = mainTabPage;
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = settingsTabPage;
        }

        private void fetchBooksButton_Click(object sender, EventArgs e)
        {
            coverPreview.Image = null;
            bookDetailsLabel.Text = "";
            downloadButton.Enabled = false;

            GetBooks();
        }

        private void hangChecker_Tick(object sender, EventArgs e)
        {
            if(jobAliveCheck == 0)
            {
                jobAliveCheck = 1;
                WriteToLogs("Detected download hang, attempting to revive job...");
                browser.Back();
            }
            else if(jobAliveCheck == 1)
            {
                jobAliveCheck = 0;
            }
        }

        private void bookListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (jobCurrBookPage == -1)
            {
                try
                {
                    coverPreview.Image = booksListView.SelectedItems[0].ImageList.Images[0];
                    bookDetailsLabel.Text = booksListView.SelectedItems[0].Text;
                    downloadButton.Enabled = true;
                }
                catch (ArgumentOutOfRangeException)
                {
                    coverPreview.Image = null;
                    bookDetailsLabel.Text = "";
                    downloadButton.Enabled = false;
                }
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //This method fakes the browser being in the tabpage when using debug mode.
            //The browser control actually can't be in the tabpage because it causes problems when loading pages.
            //This method makes it act like its in the tabpage but its actually not.
            if(tabControl1.SelectedTab == browserTabPage)
            {
                debugBrowser.BringToFront();
            }
            else
            {
                debugBrowser.SendToBack();
            }
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            fetchBooksButton.Enabled = false;
            downloadButton.Enabled = false;
            string[] bookDetails = bookDetailsLabel.Text.Split(new[] { "\n" }, StringSplitOptions.None);

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to download\r\n\r\n" + bookDetails[0] + " by " + bookDetails[1] + " (eISBN: " + bookDetails[2] + ") \r\n\r\nto a PDF?", "Download Confirmation", MessageBoxButtons.YesNo);
            WriteToLogs("Awaiting download confirmation of " + bookDetails[0] + " by " + bookDetails[1] + " (eISBN: " + bookDetails[2] + ").");
            if (dialogResult == DialogResult.Yes)
            {
                jobStartTime = DateTime.Now;

                jobCurrBookPage = 0;

                jobBookTitle = bookDetails[0];
                jobBookAuthor = bookDetails[1];
                jobBookEISBN = bookDetails[2];

                System.IO.Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + jobBookEISBN + "\\");

                WriteToLogs("Starting download job of " + jobBookTitle + " by " + jobBookAuthor + " (eISBN: " + jobBookEISBN + ").");
                browser.Load("https://ereader.chegg.com/#/books/" + jobBookEISBN + "/cfi/" + jobCurrBookPage + "!/4/2@100:0.00");
            }
            else
            {
                fetchBooksButton.Enabled = true;
                downloadButton.Enabled = true;
                WriteToLogs("Canceling download job of " + bookDetails[0] + " by " + bookDetails[1] + " (eISBN: " + bookDetails[2] + ").");
            }
        }
    }
}
