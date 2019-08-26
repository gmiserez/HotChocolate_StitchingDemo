using System;
using System.Collections.Generic;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using HotChocolate.Resolvers;
using Microsoft.Extensions.DiagnosticAdapter;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Demo
{
    public class HotChocolateLogger
            : IDiagnosticObserver
    {
        private readonly ILogger<HotChocolateLogger> _logger;

        public HotChocolateLogger(ILogger<HotChocolateLogger> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [DiagnosticName("HotChocolate.Execution.Query")]
        public void QueryExecute()
        {
            // This method is required to enable recording "Query.Start" and
            // "Query.Stop" diagnostic events. Do not write code in here.
        }

        [DiagnosticName("HotChocolate.Execution.Query.Start")]
        public void BeginQueryExecute(IQueryContext context)
        {
            _logger.LogInformation("Begin:\r\n{0}", context.Request.Query);
        }

        [DiagnosticName("HotChocolate.Execution.Query.Stop")]
        public void EndQueryExecute(
            IQueryContext context,
            IExecutionResult result)
        {
            _logger.LogInformation("End:\r\n{0}", context.Request.Query);
        }

        [DiagnosticName("HotChocolate.Execution.Query.Error")]
        public virtual void OnQueryError(
            IQueryContext context,
            Exception exception)
        {
            _logger.LogError(exception, exception.Message);
        }

        [DiagnosticName("HotChocolate.Execution.Resolver.Error")]
        public virtual void OnResolverError(
            IResolverContext context,
            IEnumerable<IError> errors)
        {
            foreach (IError error in errors)
            {
                _logger.LogError(JsonConvert.SerializeObject(error));
            }
        }

        [DiagnosticName("HotChocolate.Execution.Validation.Error")]
        public virtual void OnValidationError(
            IQueryContext context,
            IReadOnlyCollection<IError> errors)
        {
            foreach (IError error in errors)
            {
                _logger.LogError(JsonConvert.SerializeObject(error));
            }
        }
    }
}
