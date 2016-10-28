using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsServer.Model;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;

namespace NewsServer.Queues.Articles
{
    public class AzureQueue : Interfaces.IArticleQueueContext
    {
        private CloudQueue _queue;

        public AzureQueue()
        {
            StorageCredentials storageCred = new StorageCredentials("adhackstorage", "JcOWSp+dbZ67Ity3KcgqfMnygsK/R6+gdAWfkcx/IaJ3+pe1aD43o1G5WN/Bm0jH+HZ+R6FnjMPK8zyQ8w==");
            CloudStorageAccount acct = new CloudStorageAccount(storageCred, true);
            CloudQueueClient qcli = acct.CreateCloudQueueClient();
            _queue = qcli.GetQueueReference("allarticlesqueue"); //must be lowercase!!
            bool tmpResult =_queue.CreateIfNotExistsAsync().Result;
        }

        public string Peek()
        {
            CloudQueueMessage msg = _queue.PeekMessageAsync().Result;
            if (msg != null)
            {
                string json = msg.AsString;
                try
                {
                    return json;
                }
                catch (Exception ex)
                {
                    string exception = ex.ToString();
                    //deserialisation failed
                    return null;
                }


            }
            else
            {

                return null;
            }

        }
    

        public string Pop()
        {
            CloudQueueMessage msg = _queue.GetMessageAsync().Result;
            if(msg!= null)
            {
                string json = msg.AsString;
                _queue.DeleteMessageAsync(msg);
                try
                {
                    return json;
                }
                catch(Exception ex)
                {
                    string exception = ex.ToString();
                    //deserialisation failed
                    return null;
                }
            

            }
            else
            {

                return null;
            }

        }

        public void Push(string queueItem)
        {
            string json = queueItem;
            CloudQueueMessage msg = new CloudQueueMessage(json);
            _queue.AddMessageAsync(msg);
        }
    }
}
