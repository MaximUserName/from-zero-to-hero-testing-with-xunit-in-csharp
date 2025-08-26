using System.Diagnostics;
using System.Text;

namespace OutputDiagnostics.Exercise;

public class PaymentProcessorTests
{
    private readonly ITestOutputHelper _output;

    public PaymentProcessorTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(99.99, "USD", "CreditCard", "CUST-123", true, "Standard credit card payment")]
    [InlineData(5000.00, "USD", "BankTransfer", "CUST-456", true, "High-value bank transfer")]
    [InlineData(-100.00, "USD", "CreditCard", "CUST-789", false, "Invalid negative amount")]
    [InlineData(15000.00, "USD", "CreditCard", "CUST-999", false, "Amount exceeds processing limit")]
    public void ProcessPayment_ComprehensiveScenarios_WithDetailedDiagnostics(
        decimal amount, string currency, string paymentMethod, string customerId, 
        bool expectedSuccess, string scenario)
    {
        // === PHASE 1: Test Setup and Environment Diagnostics ===
        _output.WriteLine("=".PadRight(70, '='));
        _output.WriteLine($"PAYMENT PROCESSING TEST: {scenario.ToUpper()}");
        _output.WriteLine("=".PadRight(70, '='));
        
        LogTestEnvironment();
        LogTestParameters(amount, currency, paymentMethod, customerId, expectedSuccess, scenario);
        
        var testStartTime = DateTime.UtcNow;
        var stopwatch = Stopwatch.StartNew();
        var initialMemory = GC.GetTotalMemory(false);
        
        // === PHASE 2: System Under Test Setup ===
        _output.WriteLine("\n--- SYSTEM SETUP ---");
        var paymentProcessor = new PaymentProcessor();
        var initialDiagnostics = paymentProcessor.GetSystemDiagnostics();
        
        _output.WriteLine("PaymentProcessor instance created");
        _output.WriteLine($"Initial system state:");
        foreach (var kvp in initialDiagnostics)
        {
            _output.WriteLine($"  {kvp.Key}: {kvp.Value}");
        }
        
        // Create payment object
        var payment = new Payment
        {
            TransactionId = "TXN-001",
            Amount = amount,
            Currency = currency,
            PaymentMethod = paymentMethod,
            CustomerId = customerId
        };
        
        // === PHASE 3: Execution with Comprehensive Monitoring ===
        PaymentProcessingResult? result = null;
        Exception? caughtException = null;
        
        try
        {
            _output.WriteLine($"\n--- EXECUTION PHASE ---");
            _output.WriteLine($"Processing payment:");
            _output.WriteLine($"  Transaction ID: {payment.TransactionId}");
            _output.WriteLine($"  Amount: {payment.Amount:C}");
            _output.WriteLine($"  Currency: {payment.Currency}");
            _output.WriteLine($"  Payment Method: {payment.PaymentMethod}");
            _output.WriteLine($"  Customer ID: {payment.CustomerId}");
            
            result = paymentProcessor.ProcessPayment(payment);
            
            _output.WriteLine($"Payment processing completed:");
            _output.WriteLine($"  Success: {result.IsSuccess}");
            _output.WriteLine($"  Transaction ID: {result.TransactionId ?? "N/A"}");
            _output.WriteLine($"  Processing Time: {result.ProcessingTimeMs}ms");
            
            if (!string.IsNullOrEmpty(result.ExternalServiceResponse))
            {
                _output.WriteLine($"  External Service Response: {result.ExternalServiceResponse}");
            }
        }
        catch (Exception ex)
        {
            caughtException = ex;
            _output.WriteLine($"Payment processing failed with exception:");
            _output.WriteLine($"  Type: {ex.GetType().Name}");
            _output.WriteLine($"  Message: {ex.Message}");
            _output.WriteLine($"  Stack Trace: {ex.StackTrace}");
        }
        finally
        {
            stopwatch.Stop();
        }
        
        // === PHASE 4: Performance and Memory Analysis ===
        var finalMemory = GC.GetTotalMemory(false);
        var memoryDelta = finalMemory - initialMemory;
        
        LogPerformanceMetrics(stopwatch.ElapsedMilliseconds, memoryDelta);
        
        // === PHASE 5: System State Analysis ===
        if (result != null)
        {
            _output.WriteLine($"\n--- SYSTEM STATE ANALYSIS ---");
            _output.WriteLine($"Captured system state during processing:");
            foreach (var kvp in result.SystemState)
            {
                _output.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }
        }
        
        var finalDiagnostics = paymentProcessor.GetSystemDiagnostics();
        _output.WriteLine($"Final system diagnostics:");
        foreach (var kvp in finalDiagnostics)
        {
            _output.WriteLine($"  {kvp.Key}: {kvp.Value}");
        }
        
        // === PHASE 6: Comprehensive Diagnostic Reporting ===
        var diagnosticReport = GenerateComprehensiveDiagnosticReport(
            payment, result, caughtException, stopwatch.ElapsedMilliseconds, memoryDelta, scenario);
        
        _output.WriteLine(diagnosticReport);
        
        // === PHASE 7: Assertions with Rich Context ===
        if (expectedSuccess)
        {
            Assert.Null(caughtException);
            
            Assert.NotNull(result);
            Assert.True(result.IsSuccess, 
                $"Payment should succeed for {scenario}. " +
                $"Amount: {amount:C}, Method: {paymentMethod}, " +
                $"Customer: {customerId}, Error: {result.ErrorMessage}");
            
            Assert.NotNull(result.TransactionId);
            Assert.True(result.ProcessingTimeMs > 0, "Processing time should be recorded");
            Assert.Equal(amount, result.ProcessedAmount);
            Assert.NotNull(result.ExternalServiceResponse);
            
            _output.WriteLine($"✓ All success assertions passed for: {scenario}");
        }
        else
        {
            Assert.NotNull(result);
            Assert.False(result.IsSuccess, 
                $"Payment should fail for {scenario}. " +
                $"Amount: {amount:C}, Method: {paymentMethod}, Customer: {customerId}");
            
            Assert.NotNull(result.ErrorMessage);
            Assert.Null(result.TransactionId);
            _output.WriteLine($"✓ Expected failure occurred: {result.ErrorMessage}");
        }
        
        // === PHASE 8: Service State Verification ===
        var processedPayments = paymentProcessor.GetProcessedPayments();
        _output.WriteLine($"\n--- SERVICE STATE VERIFICATION ---");
        _output.WriteLine($"Total processed payments in service: {processedPayments.Count}");
        
        if (expectedSuccess)
        {
            Assert.True(processedPayments.Count > 0, "Successful payments should be recorded");
            _output.WriteLine($"✓ Payment recorded in service state");
        }
        
        _output.WriteLine($"\nTest completed in {stopwatch.ElapsedMilliseconds}ms");
        _output.WriteLine($"Memory impact: {memoryDelta / 1024:N0} KB");
        _output.WriteLine("=".PadRight(70, '='));
    }

    [Fact]
    public void ProcessPayment_PerformanceUnderLoad_WithDetailedMetrics()
    {
        _output.WriteLine("=== PAYMENT PROCESSOR PERFORMANCE LOAD TEST ===");
        
        var paymentProcessor = new PaymentProcessor();
        var paymentCount = 25;
        var results = new List<PaymentProcessingResult>();
        
        var overallStopwatch = Stopwatch.StartNew();
        var initialMemory = GC.GetTotalMemory(false);
        
        _output.WriteLine($"Processing {paymentCount} payments for performance analysis...");
        _output.WriteLine($"Initial memory: {initialMemory / 1024:N0} KB");
        
        for (int i = 1; i <= paymentCount; i++)
        {
            var payment = new Payment
            {
                TransactionId = $"LOAD-TEST-{i:D3}",
                Amount = Random.Shared.Next(100, 5000),
                Currency = "USD",
                PaymentMethod = i % 2 == 0 ? "CreditCard" : "BankTransfer",
                CustomerId = $"LOAD-CUST-{1000 + i}"
            };
            
            var result = paymentProcessor.ProcessPayment(payment);
            results.Add(result);
            
            // Log progress every 5 payments
            if (i % 5 == 0)
            {
                var currentMemory = GC.GetTotalMemory(false);
                var successCount = results.Count(r => r.IsSuccess);
                _output.WriteLine($"Progress: {i}/{paymentCount} payments processed. " +
                                  $"Success rate: {successCount}/{results.Count} ({(double)successCount/results.Count*100:F1}%). " +
                                  $"Memory: {currentMemory / 1024:N0} KB");
            }
        }
        
        overallStopwatch.Stop();
        var finalMemory = GC.GetTotalMemory(false);
        
        // Comprehensive performance analysis
        var successfulPayments = results.Count(r => r.IsSuccess);
        var failedPayments = results.Count(r => !r.IsSuccess);
        var avgProcessingTime = results.Average(r => r.ProcessingTimeMs);
        var maxProcessingTime = results.Max(r => r.ProcessingTimeMs);
        var minProcessingTime = results.Min(r => r.ProcessingTimeMs);
        var totalSimulatedProcessingTime = results.Sum(r => r.ProcessingTimeMs);
        
        _output.WriteLine($"\n=== COMPREHENSIVE PERFORMANCE ANALYSIS ===");
        _output.WriteLine($"Load Test Summary:");
        _output.WriteLine($"  Total payments processed: {paymentCount}");
        _output.WriteLine($"  Successful payments: {successfulPayments}");
        _output.WriteLine($"  Failed payments: {failedPayments}");
        _output.WriteLine($"  Success rate: {(double)successfulPayments/paymentCount*100:F2}%");
        
        _output.WriteLine($"\nTiming Analysis:");
        _output.WriteLine($"  Overall test execution time: {overallStopwatch.ElapsedMilliseconds}ms");
        _output.WriteLine($"  Total simulated processing time: {totalSimulatedProcessingTime}ms");
        _output.WriteLine($"  Average processing time per payment: {avgProcessingTime:F2}ms");
        _output.WriteLine($"  Minimum processing time: {minProcessingTime}ms");
        _output.WriteLine($"  Maximum processing time: {maxProcessingTime}ms");
        _output.WriteLine($"  Throughput: {paymentCount / (overallStopwatch.ElapsedMilliseconds / 1000.0):F2} payments/second");
        
        _output.WriteLine($"\nMemory Analysis:");
        _output.WriteLine($"  Initial memory: {initialMemory / 1024:N0} KB");
        _output.WriteLine($"  Final memory: {finalMemory / 1024:N0} KB");
        _output.WriteLine($"  Memory delta: {(finalMemory - initialMemory) / 1024:N0} KB");
        _output.WriteLine($"  Memory per payment: {(finalMemory - initialMemory) / paymentCount / 1024:F2} KB");
        
        _output.WriteLine($"\nGarbage Collection Analysis:");
        _output.WriteLine($"  Gen0 collections: {GC.CollectionCount(0)}");
        _output.WriteLine($"  Gen1 collections: {GC.CollectionCount(1)}");
        _output.WriteLine($"  Gen2 collections: {GC.CollectionCount(2)}");
        
        // Performance assertions
        Assert.True(successfulPayments > 0, "At least some payments should succeed");
        Assert.True(overallStopwatch.ElapsedMilliseconds < 30000, 
            $"Load test should complete within 30 seconds. Actual: {overallStopwatch.ElapsedMilliseconds}ms");
        Assert.True(avgProcessingTime > 0, "Average processing time should be recorded");
        
        _output.WriteLine($"\n✓ Performance load test completed successfully");
        _output.WriteLine("=".PadRight(70, '='));
    }

    private void LogTestEnvironment()
    {
        _output.WriteLine($"\n--- ENVIRONMENT DIAGNOSTICS ---");
        _output.WriteLine($"Machine: {Environment.MachineName}");
        _output.WriteLine($"OS Version: {Environment.OSVersion}");
        _output.WriteLine($"Process ID: {Environment.ProcessId}");
        _output.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}");
        _output.WriteLine($"Processor Count: {Environment.ProcessorCount}");
        _output.WriteLine($"UTC Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff}");
        _output.WriteLine($"Working Set: {Environment.WorkingSet / 1024 / 1024:N0} MB");
    }

    private void LogTestParameters(decimal amount, string currency, string paymentMethod, 
        string customerId, bool expectedSuccess, string scenario)
    {
        _output.WriteLine($"\n--- TEST PARAMETERS ---");
        _output.WriteLine($"Scenario: {scenario}");
        _output.WriteLine($"Amount: {amount:C}");
        _output.WriteLine($"Currency: {currency}");
        _output.WriteLine($"Payment Method: {paymentMethod}");
        _output.WriteLine($"Customer ID: {customerId}");
        _output.WriteLine($"Expected Success: {expectedSuccess}");
    }

    private void LogPerformanceMetrics(long elapsedMs, long memoryDelta)
    {
        _output.WriteLine($"\n--- PERFORMANCE METRICS ---");
        _output.WriteLine($"Execution Time: {elapsedMs}ms");
        _output.WriteLine($"Memory Delta: {memoryDelta / 1024:N0} KB");
        _output.WriteLine($"GC Collections during test:");
        _output.WriteLine($"  Generation 0: {GC.CollectionCount(0)}");
        _output.WriteLine($"  Generation 1: {GC.CollectionCount(1)}");
        _output.WriteLine($"  Generation 2: {GC.CollectionCount(2)}");
    }

    private string GenerateComprehensiveDiagnosticReport(Payment payment, PaymentProcessingResult? result, 
        Exception? exception, long elapsedMs, long memoryDelta, string scenario)
    {
        var report = new StringBuilder();
        
        report.AppendLine($"\n" + "=".PadRight(70, '='));
        report.AppendLine($"COMPREHENSIVE DIAGNOSTIC REPORT");
        report.AppendLine($"=".PadRight(70, '='));
        
        report.AppendLine($"Report Generated: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} UTC");
        report.AppendLine($"Test Scenario: {scenario}");
        
        report.AppendLine($"\n--- INPUT PARAMETERS ---");
        report.AppendLine($"Transaction ID: {payment.TransactionId}");
        report.AppendLine($"Amount: {payment.Amount:C}");
        report.AppendLine($"Currency: {payment.Currency}");
        report.AppendLine($"Payment Method: {payment.PaymentMethod}");
        report.AppendLine($"Customer ID: {payment.CustomerId}");
        report.AppendLine($"Created At: {payment.CreatedAt:yyyy-MM-dd HH:mm:ss.fff}");
        
        report.AppendLine($"\n--- EXECUTION SUMMARY ---");
        report.AppendLine($"Status: {(exception != null ? "EXCEPTION" : result?.IsSuccess.ToString() ?? "UNKNOWN")}");
        report.AppendLine($"Total Duration: {elapsedMs}ms");
        report.AppendLine($"Memory Impact: {memoryDelta / 1024:N0} KB");
        
        if (exception != null)
        {
            report.AppendLine($"\n--- EXCEPTION DETAILS ---");
            report.AppendLine($"Exception Type: {exception.GetType().Name}");
            report.AppendLine($"Exception Message: {exception.Message}");
            report.AppendLine($"Stack Trace: {exception.StackTrace}");
        }
        else if (result != null)
        {
            report.AppendLine($"\n--- PROCESSING RESULTS ---");
            report.AppendLine($"Transaction ID: {result.TransactionId ?? "N/A"}");
            report.AppendLine($"Processing Time: {result.ProcessingTimeMs}ms");
            report.AppendLine($"Processed Amount: {result.ProcessedAmount:C}");
            report.AppendLine($"Processed At: {result.ProcessedAt:yyyy-MM-dd HH:mm:ss.fff}");
            report.AppendLine($"External Service Response: {result.ExternalServiceResponse ?? "N/A"}");
            
            if (!result.IsSuccess)
            {
                report.AppendLine($"Error Message: {result.ErrorMessage}");
            }
            
            if (result.SystemState.Any())
            {
                report.AppendLine($"\n--- CAPTURED SYSTEM STATE ---");
                foreach (var kvp in result.SystemState)
                {
                    report.AppendLine($"{kvp.Key}: {kvp.Value}");
                }
            }
        }
        
        report.AppendLine($"=".PadRight(70, '='));
        
        return report.ToString();
    }
}