using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace AsyncService.Services
{
    public class ContentFetcher
    {
        public string GetAllTheContents()
        {
            const string connectionString = "my connection string";

            var forceSynchronousSerialOptions = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 1 };
            var readRecords = new 
                TransformManyBlock<string, string>((Func<string, IEnumerable<string>>)ReadRecords, forceSynchronousSerialOptions);

            var parseRecords = new
                TransformBlock<string, string>((Func<string, string>)ParseRecords, forceSynchronousSerialOptions);

            var callApi = new
                TransformBlock<string, string>((Func<string, string>)CallApi, forceSynchronousSerialOptions);

            var content = new StringBuilder();
            var writeRecords = new
                ActionBlock<string>((record) => {                    
                    WriteRecords(record);
                    content.AppendLine(record);
                }, forceSynchronousSerialOptions);

            readRecords.LinkTo(parseRecords);
            parseRecords.LinkTo(callApi);
            callApi.LinkTo(writeRecords);

            readRecords.Completion.ContinueWith(t => parseRecords.Complete());
            parseRecords.Completion.ContinueWith(t => callApi.Complete());
            callApi.Completion.ContinueWith(t => writeRecords.Complete());

            readRecords.Post(connectionString);

            readRecords.Complete();

            writeRecords.Completion.Wait();

            return content.ToString();
        }

        private void WriteRecords(string record)
        {
            Thread.Sleep(50);
        }

        private string CallApi(string item)
        {
            Thread.Sleep(5000);

            return $"x:{item}";
        }

        private string ParseRecords(string record)
        {
            Thread.Sleep(1);

            return $"A-{record}";
        }

        private IEnumerable<string> ReadRecords(string input)
        {
            Thread.Sleep(2000);

            return Enumerable
                    .Range(0, 100)
                    .Select(x => x.ToString());            
        }
    }
}