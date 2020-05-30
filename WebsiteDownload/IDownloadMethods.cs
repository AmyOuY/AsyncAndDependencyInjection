using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WPFDemo.Library.Models;

namespace WPFDemo.Library
{
    public interface IDownloadMethods
    {
        List<WebsiteDataModel> DownloadSyncWithProgress(IProgress<ProgressReportModel> progress, CancellationToken cancellationToken);
        List<WebsiteDataModel> DownloadParallelSync();
        Task<List<WebsiteDataModel>> DownloadParallelAsyncWithProgress(IProgress<ProgressReportModel> progress);
        Task<List<WebsiteDataModel>> DownloadASyncWithProgress(IProgress<ProgressReportModel> progress, CancellationToken cancellationToken);
        Task<List<WebsiteDataModel>> DownLoadParallelAsync();
        WebsiteDataModel DownloadWebsite(string url);
        Task<WebsiteDataModel> DownloadWebsiteAsync(string url);
        List<string> GetWebsites();
    }
}