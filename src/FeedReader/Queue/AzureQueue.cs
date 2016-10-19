using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedReader.Model;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;

namespace FeedReader.Queue
{
    public class AzureQueue : Interfaces.IQueueContext
    {
        private CloudQueue _queue;

        public AzureQueue()
        {
            StorageCredentials storageCred = new StorageCredentials("adhackstorage", "uKeYS/S2MvF27ZxoA8BZ+3rqC1lUHBHGBwRybMAAldEOw+S3yrkKqhLnsL2yQWY4uJ9C76BeVjNmxcdtaIJJHw==");
            CloudStorageAccount acct = new CloudStorageAccount(storageCred, true);
            CloudQueueClient qcli = acct.CreateCloudQueueClient();
            _queue = qcli.GetQueueReference("allscrapequeue"); //must be lowercase!!
            _queue.CreateIfNotExistsAsync();
        }

        public ScrapeQueue Peek()
        {
            CloudQueueMessage msg = _queue.PeekMessageAsync().Result;
            if (msg != null)
            {
                string json = msg.AsString;
                try
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<ScrapeQueue>(json);
                }
                catch (Exception ex)
                {
                    string exeption = ex.ToString();
                    //deserialisation failed
                    return null;
                }


            }
            else
            {

                return null;
            }

        }
    

        public ScrapeQueue Pop()
        {
            CloudQueueMessage msg = _queue.GetMessageAsync().Result;
            if(msg!= null)
            {
                _queue.DeleteMessageAsync(msg);
                string json = msg.AsString;
                try
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<ScrapeQueue>(json);
                }
                catch(Exception ex)
                {
                    string exeption = ex.ToString();
                    //deserialisation failed
                    return null;
                }
            

            }
            else
            {

                return null;
            }

        }

        public void Push(ScrapeQueue queueItem)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(queueItem);
            CloudQueueMessage msg = new CloudQueueMessage(json);
            _queue.AddMessageAsync(msg);
        }
    }
}
