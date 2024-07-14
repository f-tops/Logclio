# Logclio
Loglio is a log processing service made with .NET Core 8. It provides an API for uploading log files, processes these files using a background service, and stores the log entries in memory for quick searching.

## Project Structure

### Logclio.Api: 
Contains the API project with controllers and configuration files.

### Logclio.Domain: 
Contains the core domain logic, including models, services, repositories, and background processing services.

### Logclio.Test: 
Contains the unit tests for the API.

# Log File Processing
###    Background Processing: 
The LogFileBackgroundProcessorService runs in the background, dequeuing log files from the queue service and using the processor factory to process and store them in the in-memory log store. This ensures that log file processing is handled asynchronously and efficiently, improving overall application performance and responsiveness.
###    Log File Queue: 
The project implements a queue service (LogFileQueueService) to enqueue log files for processing. This decouples the file upload process from the actual processing, improving scalability and responsiveness.
###    Log Processor Factory: 
The LogProcessorFactory dynamically creates the appropriate log file processor based on the file extension, supporting extensibility for different log file formats.
# In-Memory Storage
###    In-Memory Log Store: 
The project uses in-memory storage to quickly store and retrieve log entries. This improves performance for searching and analyzing log data without the overhead of persistent storage.

## Using the Swagger UI
The Swagger UI provides an interactive interface to explore and test the Logclio API.
Swagger is available on https://localhost:5000/swagger/index.html
