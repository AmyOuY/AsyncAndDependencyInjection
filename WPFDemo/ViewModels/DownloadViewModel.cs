using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPFDemo.Library;
using WPFDemo.Library.Models;

namespace WPFDemo.ViewModels
{
    public class DownloadViewModel : Screen
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        private readonly IDownloadMethods _downloadMethods; 

        public DownloadViewModel(IDownloadMethods downloadMethods)
        {
            _downloadMethods = downloadMethods;
        }


       
        public void ExecuteSync()
        {
            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                var results = _downloadMethods.DownloadSyncWithProgress(progress, cts.Token);
                PrintResults(results);
            }
            catch (OperationCanceledException)
            {

                ResultWindow += $"The sync download was cancelled { Environment.NewLine }";
            }

            watch.Stop();

            var elapsedMS = watch.ElapsedMilliseconds;

            ResultWindow += $"Total execution time: { elapsedMS }";
        }


        public void ExecuteParallelSync()
        {
            
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var results = _downloadMethods.DownloadParallelSync();
            PrintResults(results);

            watch.Stop();

            var elapsedMS = watch.ElapsedMilliseconds;

            ResultWindow += $"Total execution time: { elapsedMS }";
        }        


        public async Task ExecuteAsync()
        {
            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                var results = await _downloadMethods.DownloadASyncWithProgress(progress, cts.Token);               
                PrintResults(results);
            }
            catch (OperationCanceledException)
            {

                ResultWindow += $"The async download was cancelled { Environment.NewLine }";
            }

            watch.Stop();

            var elapsedMS = watch.ElapsedMilliseconds;

            ResultWindow += $"Total execution time: { elapsedMS }";
        }

        
        public async Task ExecuteParallelAsync()
        {           
            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            var results = await _downloadMethods.DownloadParallelAsyncWithProgress(progress);
            //var results = await _downloadMethods.DownLoadParallelAsync();
            PrintResults(results);

            watch.Stop();

            var elapsedMS = watch.ElapsedMilliseconds;

            ResultWindow += $"Total execution time: { elapsedMS }";

        }


        public void Cancellation()
        {
            cts.Cancel();
        }


        private string _resultWindow;
        public string ResultWindow
        {
            get { return _resultWindow; }
            set
            {
                _resultWindow = value;
                NotifyOfPropertyChange(() => ResultWindow);
            }
        }


        private int _progressValue;
        public int ProgressValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                NotifyOfPropertyChange(() => ProgressValue);
            }
        }


        private void ReportProgress(object sender, ProgressReportModel e)
        {
            ProgressValue = e.PercentageCompleted;
            PrintResults(e.Downloaded);
        }

        private void PrintResults(List<WebsiteDataModel> results)
        {
            ResultWindow = "";

            foreach (var result in results)
            {
                ResultWindow += $"{ result.WebsiteURL } downloaded: { result.WebsiteData.Length } characters { Environment.NewLine}";
            }
        }

    }
}
