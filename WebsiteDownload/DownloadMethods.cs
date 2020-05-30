using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WPFDemo.Library.Models;

namespace WPFDemo.Library
{
    public class DownloadMethods : IDownloadMethods
    {
        public List<string> GetWebsites()
        {
            List<string> output = new List<string>();

            output.Add("https://www.google.com");
            output.Add("https://www.msn.com");
            output.Add("https://www.facebook.com");
            output.Add("https://www.stackoverflow.com");
            output.Add("https://www.yahoo.com");
            output.Add("https://youtube.com");

            return output;
        }


        public List<WebsiteDataModel> DownloadSyncWithProgress(IProgress<ProgressReportModel> progress, CancellationToken cancellationToken)
        {
            List<string> websites = GetWebsites();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();
            ProgressReportModel report = new ProgressReportModel();

            foreach (string site in websites)
            {
                WebsiteDataModel results = DownloadWebsite(site);
                output.Add(results);              

                cancellationToken.ThrowIfCancellationRequested();

                report.PercentageCompleted = (output.Count * 100) / websites.Count;
                report.Downloaded = output;
                progress.Report(report);
            }

            return output;
        }



        public List<WebsiteDataModel> DownloadParallelSync()
        {
            List<string> websites = GetWebsites();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();

            Parallel.ForEach(websites, (site) =>
            {
                WebsiteDataModel results = DownloadWebsite(site);
                output.Add(results);
            });

            return output;
        }


        public async Task<List<WebsiteDataModel>> DownloadParallelAsyncWithProgress(IProgress<ProgressReportModel> progress)
        {
            List<string> websites = GetWebsites();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();
            ProgressReportModel report = new ProgressReportModel();

            await Task.Run(() =>
            {
                Parallel.ForEach<string>(websites, (site) =>
                {
                    WebsiteDataModel results = DownloadWebsite(site);
                    output.Add(results);

                    report.PercentageCompleted = (output.Count * 100) / websites.Count;
                    report.Downloaded = output;
                    progress.Report(report);
                });
            });

            return output;
        }



        public async Task<List<WebsiteDataModel>> DownloadASyncWithProgress(IProgress<ProgressReportModel> progress, CancellationToken cancellationToken)
        {
            List<string> websites = GetWebsites();
            List<WebsiteDataModel> output = new List<WebsiteDataModel>();
            ProgressReportModel report = new ProgressReportModel();

            foreach (string site in websites)
            {
                WebsiteDataModel results = await DownloadWebsiteAsync(site);
                output.Add(results);

                cancellationToken.ThrowIfCancellationRequested();

                report.PercentageCompleted = (output.Count * 100) / websites.Count;
                report.Downloaded = output;
                progress.Report(report);
            }

            return output;
        }


        public async Task<List<WebsiteDataModel>> DownLoadParallelAsync()
        {
            List<string> websites = GetWebsites();
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();

            foreach (string site in websites)
            {
                tasks.Add(DownloadWebsiteAsync(site));
            }

            var results = await Task.WhenAll(tasks);

            return new List<WebsiteDataModel>(results);
        }




        public WebsiteDataModel DownloadWebsite(string url)
        {
            WebsiteDataModel data = new WebsiteDataModel();
            WebClient client = new WebClient();

            data.WebsiteURL = url;
            data.WebsiteData = client.DownloadString(url);

            return data;
        }


        public async Task<WebsiteDataModel> DownloadWebsiteAsync(string url)
        {
            WebsiteDataModel data = new WebsiteDataModel();
            WebClient client = new WebClient();

            data.WebsiteURL = url;
            data.WebsiteData = await client.DownloadStringTaskAsync(url);

            return data;
        }
    }
}
