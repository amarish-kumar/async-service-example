using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AsyncService.Services
{
    public class ContentFetcher
    {
        public string GetAllTheContents()
        {
            const string connectionString = "my connection string";

            var asyncParallelOptions = new ExecutionDataflowBlockOptions
            {
                BoundedCapacity = 1000,
                MaxDegreeOfParallelism = 200
            };
            var readRecords = new 
                TransformManyBlock<string, string>((Func<string, Task<IEnumerable<string>>>)ReadRecords, asyncParallelOptions);

            var parseRecords = new
                TransformBlock<string, string>((Func<string, string>)ParseRecords, asyncParallelOptions);

            var callApi = new
                TransformBlock<string, string>((Func<string, Task<string>>)CallApi, asyncParallelOptions);

            var content = new StringBuilder();
            var writeRecords = new
                ActionBlock<string>(async (record) => {                    
                    await WriteRecords(record);
                    content.AppendLine(record);
                }, asyncParallelOptions);

            readRecords.LinkTo(parseRecords);
            parseRecords.LinkTo(callApi);
            callApi.LinkTo(writeRecords);

            readRecords.Completion.ContinueWith(t => parseRecords.Complete());
            parseRecords.Completion.ContinueWith(t => callApi.Complete());
            callApi.Completion.ContinueWith(t => writeRecords.Complete());

            readRecords.SendAsync(connectionString);

            readRecords.Complete();

            writeRecords.Completion.Wait();

            return content.ToString();
        }

        private async Task WriteRecords(string record)
        {
            await Task.Delay(50);
        }

        private async Task<string> CallApi(string item)
        {
            await Task.Delay(5000);

            return $"x:{item}";
        }

        private string ParseRecords(string record)
        {
            Thread.Sleep(1);

            return $"A-{record}";
        }

        private async Task<IEnumerable<string>> ReadRecords(string input)
        {
            await Task.Delay(2000);

            return Enumerable
                    .Range(0, 100)
                    .Select(x => x.ToString());            
        }
    }
}