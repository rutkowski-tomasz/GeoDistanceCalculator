using System;
using System.Linq;
using FluentAssertions;
using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using Xunit;

namespace Application.Tests.Unit.Swagger;

public class OperationSummaryTests
{
    [Fact]
    public void EachOperationProcessorShouldSetCustomSummary()
    {
        typeof(IApplicationMarker)
            .Assembly
            .GetExportedTypes()
            .Where(p => p.IsAssignableTo(typeof(IOperationProcessor)))
            .Select(Activator.CreateInstance)
            .Cast<IOperationProcessor>()
            .Select(ProcessAndCheckIfSummaryIsUpdated)
            .Should()
            .AllBeEquivalentTo(true);
    }

    private bool ProcessAndCheckIfSummaryIsUpdated(IOperationProcessor processor)
    {
        var context = CreateProcessorContext();
        processor.Process(context);
        return !string.IsNullOrEmpty(context.OperationDescription.Operation.Summary);
    }

    private OperationProcessorContext CreateProcessorContext()
    {
        return new OperationProcessorContext(
            new OpenApiDocument(),
            new OpenApiOperationDescription()
            {
                Operation = new OpenApiOperation()
            },
            null,
            null,
            null,
            null,
            null,
            null,
            null
        );
    }
}