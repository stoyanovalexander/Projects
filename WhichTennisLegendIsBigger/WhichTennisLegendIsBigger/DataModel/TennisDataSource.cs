﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.Networking;
using Windows.Networking.Connectivity;

// The Project extracts data from wikipedia. Extract from there info for all tennis players with more than 5 grand slam titles.
// By this info compare by differnt components each two of this player one vs one by choice of the user, and on the end shows who
// of them is bigger. First time consume the services and starting about 10 seconds. After that use local storage and saves 
//there info. The info is changed four times each year after the four tournaments. The application check and consume services again
// only if there are need to do it. So starts slow only four times in the year and the first time. It works and when there are no
// internet with the last time saved data in the local storage. 
namespace WhichTennisLegendIsBigger.Data
{
    /// <summary>
    /// Creates a collection of groups and items.
    /// </summary>
    public sealed class TennisDataSource
    {
        private static TennisDataSource _TennisDataSource = new TennisDataSource();

        private ObservableCollection<TennisDataGroup> _allGroups = new ObservableCollection<TennisDataGroup>();
        public ObservableCollection<TennisDataGroup> AllGroups
        {
            get { return this._allGroups; }
        }

        public static IEnumerable<TennisDataGroup> GetGroups(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");

            return _TennisDataSource.AllGroups;
        }

        public static TennisDataGroup GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _TennisDataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static TennisDataItem GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _TennisDataSource.AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static bool IsInternet()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }

        private static Task<string> MakeAsyncRequest(string file)
        {
            var request = WebRequest.CreateHttp(file);
            request.ContentType = "application/json";
            request.Method = "GET";

            var task = Task.Factory.FromAsync(request.BeginGetResponse,
                   (asyncResult) => request.EndGetResponse(asyncResult),
                   (object)null);

            return task.ContinueWith(t => ReadStreamFromResponse(t.Result));
        }

        private static string ReadStreamFromResponse(WebResponse response)
        {
            string strContent = null;
            using (var responseStream = response.GetResponseStream())
            using (var sr = new StreamReader(responseStream))
            {
                strContent = sr.ReadToEnd();

                return strContent;
            }
        }

        private async void WriteToLocalFolder(string dataName, string data)
        {
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            var stFile = await localFolder.CreateFileAsync(dataName + ".txt",
                Windows.Storage.CreationCollisionOption.ReplaceExisting);

            await Windows.Storage.FileIO.WriteTextAsync(stFile, data);
        }

        private async Task<string> ReadDataFromLocalFolder(string name)
        {
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var stFile = await localFolder.GetFileAsync(name + ".txt");

            var data = await Windows.Storage.FileIO.ReadTextAsync(stFile);

            return data;
        }

        public TennisDataSource()
        {
            this.PopulateData();
        }

        public DateTime CheckNearestDate(DateTime[] changeInfoDate, DateTime? fromFile)
        {
            var currDate = DateTime.Now;
            // If curent data has the value from the rows under the application will starting near 10 seconds becouse will be parsing
            //the new data from internet from next time when start the application will work fast again with the local storage data 
            //!!! Must make the same change and in the: OverwriteInfo() method

            currDate = new DateTime(DateTime.Now.Year+1, 2, 8);
            //currDate = new DateTime(DateTime.Now.Year+1, 6, 20);
            //currDate = new DateTime(DateTime.Now.Year+1, 7, 18);
            //currDate = new DateTime(DateTime.Now.Year+1, 9, 19);
            //currDate = new DateTime(DateTime.Now.Year+2, 9, 19);

            //The dates of info change
            //(DateTime.Now.Year, 2, 8);    (DateTime.Now.Year, 6, 20)  (DateTime.Now.Year, 7, 18);  (DateTime.Now.Year, 9, 19);

            if (fromFile == null)
            {
                if ((currDate.Month == 2 && currDate.Day > 7) ||
                   (currDate.Month > 2 && currDate.Month < 6) ||
                   (currDate.Month == 6 && currDate.Day <= 19))
                {
                    return changeInfoDate[0];
                }
                if ((currDate.Month == 6 && currDate.Day > 19) ||
                   (currDate.Month == 7 && currDate.Day <= 17))
                {
                    return changeInfoDate[1];
                }
                if ((currDate.Month == 7 && currDate.Day > 17) ||
                  (currDate.Month > 7 && currDate.Month < 9) ||
                  (currDate.Month == 9 && currDate.Day <= 18))
                {
                    return changeInfoDate[2];
                }
                else
                {
                    return changeInfoDate[3];
                }

            }
            else
            {
               
                if ((currDate.Month == 2 && currDate.Day > 7) ||
                    (currDate.Month > 2 && currDate.Month < 6) ||
                    (currDate.Month == 6 && currDate.Day <= 19))
                {
                    return changeInfoDate[0];
                }
                if ((currDate.Month == 6 && currDate.Day > 19) ||
                   (currDate.Month == 7 && currDate.Day <= 17))
                {
                    return changeInfoDate[1];
                }
                if ((currDate.Month == 7 && currDate.Day > 17) ||
                  (currDate.Month > 7 && currDate.Month < 9) ||
                  (currDate.Month == 9 && currDate.Day <= 18))
                {
                    return changeInfoDate[2];
                }

                else
                {
                    return changeInfoDate[3];
                }
            }
        }

        private bool OverwriteInfo(DateTime? fromFile)
        {
            var currDate = DateTime.Now;

            // If curent data has the value from the rows under the application will starting near 10 seconds becouse will be parsing
            //the new data from internet from next time when start the application will work fast again with the local storage data 
            //!!! Must make the same change and in the: CheckNearestDate() method

            currDate = new DateTime(DateTime.Now.Year+1, 2, 8);
            //currDate = new DateTime(DateTime.Now.Year+1, 6, 20);
            //currDate = new DateTime(DateTime.Now.Year+1, 7, 18);
            //currDate = new DateTime(DateTime.Now.Year+1, 9, 19);
            //currDate = new DateTime(DateTime.Now.Year+2, 9, 19);

            //The dates of info change
            //(DateTime.Now.Year, 2, 8);    (DateTime.Now.Year, 6, 20)  (DateTime.Now.Year, 7, 18);  (DateTime.Now.Year, 9, 19);

            if (fromFile.Value.Month == 2 && fromFile.Value.Year==currDate.Year)
            {
                if ((currDate.Month >= 2 && currDate.Month < 6) ||
                    (currDate.Month == 6 && currDate.Day <= 19))
                {
                    return false;
                }
            }
            if (fromFile.Value.Month == 6 && fromFile.Value.Year == currDate.Year)
            {
                if ((currDate.Month == 6)||
                   (currDate.Month == 7 && currDate.Day <= 17))
                {
                    return false;
                }
            }

            if (fromFile.Value.Month == 7 && fromFile.Value.Year == currDate.Year)
            {
                if ((currDate.Month >= 7 && currDate.Month < 9) ||
                  (currDate.Month == 9 && currDate.Day <= 18))
                {
                    return false;
                }
            }

            if (fromFile.Value.Month == 9 && currDate.Year == fromFile.Value.Year)
            {
                if (currDate.Month >= 9 && currDate.Month <= 12)
                {
                    return false;
                }
            }

            if (fromFile.Value.Month == 9 && currDate.Year == fromFile.Value.Year + 1)
            {
                if (((currDate.Month == 1) ||
                 (currDate.Month == 2 && currDate.Day <= 7)))
                {
                    return false;
                }
            }
            return true;
        }

        public async void PopulateData()
        {
            string[] theName = null;
            string[] theImage = null;
            string[] theDescription = null;
            string[,] theTitlesCountComparation = null;
            string[,] theTimeBetweenComparation = null;
            string[,] theWinningYearsComparation = null;
            string[,] theTitlesValueComparation = null;
            string[,] theFinalResultComparation = null;
            int[] theTitles = null;

            DateTime australianOpen = new DateTime(DateTime.Now.Year+1, 2, 7); //For tasting change with: DateTime.Now.Year+1
            DateTime frenchOpen = new DateTime(DateTime.Now.Year+1, 6, 19);  //For tasting change with: DateTime.Now.Year+1
            DateTime winbledon = new DateTime(DateTime.Now.Year+1, 7, 17);  //For tasting change with: DateTime.Now.Year+1
            DateTime usOpen = new DateTime(DateTime.Now.Year+1, 9, 18);       //For tasting change with: DateTime.Now.Year+1

            StorageFile dateFile = null;
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            try
            {
                dateFile = await localFolder.GetFileAsync("date.txt");
            }
            catch (Exception ex)
            {
            }

            DateTime? overwriteDate = null;

            if (dateFile != null)
            {
                var result = await Windows.Storage.FileIO.ReadTextAsync(dateFile);
                overwriteDate = DateTime.Parse(result);
            }

            DateTime[] tournaments = new DateTime[]
            {
              australianOpen,
              frenchOpen,
              winbledon,
              usOpen,
            };

            if (overwriteDate != null && !this.OverwriteInfo(overwriteDate))
            {
                var namesStr = await this.ReadDataFromLocalFolder("names");
                var descriptionStr = await this.ReadDataFromLocalFolder("desc");
                var imageStr = await this.ReadDataFromLocalFolder("img");
                var titlesStr = await this.ReadDataFromLocalFolder("title");
                var titlesCountStr = await this.ReadDataFromLocalFolder("titleCount");
                var timeBetweenStr = await this.ReadDataFromLocalFolder("timeBetween");
                var winningYearsStr = await this.ReadDataFromLocalFolder("winningYears");
                var titlesValueStr = await this.ReadDataFromLocalFolder("titlesValue");
                var finalResultStr = await this.ReadDataFromLocalFolder("finalResult");

                theName = JsonConvert.DeserializeObject<string[]>(namesStr);
                theDescription = JsonConvert.DeserializeObject<string[]>(descriptionStr);
                theImage = JsonConvert.DeserializeObject<string[]>(imageStr);
                theTitles = JsonConvert.DeserializeObject<int[]>(titlesStr);
                theTitlesCountComparation = JsonConvert.DeserializeObject<string[,]>(titlesCountStr);
                theTimeBetweenComparation = JsonConvert.DeserializeObject<string[,]>(timeBetweenStr);
                theWinningYearsComparation = JsonConvert.DeserializeObject<string[,]>(winningYearsStr);
                theTitlesValueComparation = JsonConvert.DeserializeObject<string[,]>(titlesValueStr);
                theFinalResultComparation = JsonConvert.DeserializeObject<string[,]>(finalResultStr);

                //If uncoment next rows and start the application the local storage will be cleaned and the next starting of 
                //application wiil be near 10 seconds because will be extracting the data from internet

                //var stFileToCheck = await localFolder.GetFileAsync("names.txt");
                //await stFileToCheck.DeleteAsync();
                //stFileToCheck = await localFolder.GetFileAsync("desc.txt");
                //await stFileToCheck.DeleteAsync();
                //stFileToCheck = await localFolder.GetFileAsync("img.txt");
                //await stFileToCheck.DeleteAsync();
                //stFileToCheck = await localFolder.GetFileAsync("title.txt");
                //await stFileToCheck.DeleteAsync();
                //stFileToCheck = await localFolder.GetFileAsync("titleCount.txt");
                //await stFileToCheck.DeleteAsync();
                //stFileToCheck = await localFolder.GetFileAsync("timeBetween.txt");
                //await stFileToCheck.DeleteAsync();
                //stFileToCheck = await localFolder.GetFileAsync("winningYears.txt");
                //await stFileToCheck.DeleteAsync();
                //stFileToCheck = await localFolder.GetFileAsync("titlesValue.txt");
                //await stFileToCheck.DeleteAsync();
                //stFileToCheck = await localFolder.GetFileAsync("finalResult.txt");
                //await stFileToCheck.DeleteAsync();
                //await dateFile.DeleteAsync();
            }
            else
            {
                
                {
                    //SERVICES CONSUMING
                    //http://04servicesofwhichtenisistisbigger.apphb.com/

                    var please = MakeAsyncRequest("http://04servicesofwhichtenisistisbigger.apphb.com/api/Name");
                    string namesStr = please.Result;
                    theName = JsonConvert.DeserializeObject<string[]>(namesStr);

                    this.WriteToLocalFolder("names", namesStr);

                    please = MakeAsyncRequest("http://04servicesofwhichtenisistisbigger.apphb.com/api/Description");
                    string descriptionStr = please.Result;
                    theDescription = JsonConvert.DeserializeObject<string[]>(descriptionStr);

                    this.WriteToLocalFolder("desc", descriptionStr);

                    please = MakeAsyncRequest("http://04servicesofwhichtenisistisbigger.apphb.com/api/Image");
                    string imageStr = please.Result;
                    theImage = JsonConvert.DeserializeObject<string[]>(imageStr);

                    this.WriteToLocalFolder("img", imageStr);

                    please = MakeAsyncRequest("http://04servicesofwhichtenisistisbigger.apphb.com/api/Titles");
                    string titlesStr = please.Result;
                    theTitles = JsonConvert.DeserializeObject<int[]>(titlesStr);

                    this.WriteToLocalFolder("title", titlesStr);

                    please = MakeAsyncRequest("http://04servicesofwhichtenisistisbigger.apphb.com/api/TitlesCount");
                    string titlesCountStr = please.Result;
                    theTitlesCountComparation = JsonConvert.DeserializeObject<string[,]>(titlesCountStr);

                    this.WriteToLocalFolder("titleCount", titlesCountStr);

                    please = MakeAsyncRequest("http://04servicesofwhichtenisistisbigger.apphb.com/api/TimeBetween");
                    string timeBetweenStr = please.Result;
                    theTimeBetweenComparation = JsonConvert.DeserializeObject<string[,]>(timeBetweenStr);

                    this.WriteToLocalFolder("timeBetween", timeBetweenStr);

                    please = MakeAsyncRequest("http://04servicesofwhichtenisistisbigger.apphb.com/api/WinningYears");
                    string winningYearsStr = please.Result;
                    theWinningYearsComparation = JsonConvert.DeserializeObject<string[,]>(winningYearsStr);

                    this.WriteToLocalFolder("winningYears", winningYearsStr);

                    please = MakeAsyncRequest("http://04servicesofwhichtenisistisbigger.apphb.com/api/TitlesValue");
                    string titlesValueStr = please.Result;
                    theTitlesValueComparation = JsonConvert.DeserializeObject<string[,]>(titlesValueStr);

                    this.WriteToLocalFolder("titlesValue", titlesValueStr);

                    please = MakeAsyncRequest("http://04servicesofwhichtenisistisbigger.apphb.com/api/FinalResult");
                    string finalResultStr = please.Result;
                    theFinalResultComparation = JsonConvert.DeserializeObject<string[,]>(finalResultStr);

                    this.WriteToLocalFolder("finalResult", finalResultStr);

                    var date = this.CheckNearestDate(tournaments, overwriteDate);

                    this.WriteToLocalFolder("date", date.ToString());
                }
            }

            // data populations
            TennisDataGroup[] group = new TennisDataGroup[15];
            for (int i = 0; i < 15; i++)
            {
                group[i] = new TennisDataGroup("Group-" + (i + 1),
                            theName[i],
                            theName[i] + " Grand Slam titles: " + theTitles[i],
                            theImage[i],
                            theDescription[i]);
                for (int j = 0; j < 15; j++)
                {
                    if (i != j)
                    {
                        group[i].Items.Add(new TennisDataItem("Group-" + (i + 1) + "-Item-" + (j + 1),
                                 theName[j],
                                    theName[j] + " Grand Slam titles: " + theTitles[i],
                                    theImage[j],
                                    theDescription[j],
                            theTitlesCountComparation[i, j] +
                            theTimeBetweenComparation[i, j] +
                            theWinningYearsComparation[i, j] +
                            theTitlesValueComparation[i, j] +
                            theFinalResultComparation[i, j],
                                group[i]));
                    }
                }
                this.AllGroups.Add(group[i]);
            }
        }
    }
}
