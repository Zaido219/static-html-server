# Static HTML Server

A lightweight, multi-threaded static file server built entirely from scratch in C# using raw TCP sockets. 

Instead of relying on high-level HTTP abstractions (like ASP.NET Core or `HttpListener`), this project implements the fundamental networking and protocol parsing layers manually to understand how web servers actually communicate with modern browsers under the hood.

---

## 🛠️ Architecture & Design

The project is structured around clean, decoupled domain interfaces to ensure a solid separation of concerns:

* **`StaticFileServer`**: The core infrastructure piece. It initializes a `TcpListener`, listens for incoming client connections on a background loop, parses raw incoming HTTP request text, and streams back byte-perfect HTTP/1.1 formatted responses.
* **`StaticFilePipeline`**: The coordinator. It orchestrates the lifecycle of a request by sanitizing paths, fetching files, determining MIME types, and packaging the result.
* **`IPathSanitizer`**: A critical security layer that maps request paths (like `/index.html`) to the local `wwwroot` directory while actively preventing directory traversal attacks.
* **`IFileProvider`**: Abstracted file system reader that reads physical files as raw byte streams asynchronously.
* **`IMimeTypeProvider`**: Maps file extensions (`.html`, `.css`, `.js`) to their official MIME standard types so browsers know how to render them.

Unit tests for this build are implemented using C# and xUnit.

---

## 🧠 Key Takeaways & Lessons Learned

Building this server from the socket level up was an eye-opening exercise in system-level programming. Here are the core concepts I mastered while building and debugging this:

### 1. The HTTP/1.1 Protocol is Just Structured Text
Before this, HTTP felt like magic. Now I know that an HTTP response is literally just a plain-text header separated from a raw binary payload by a double line break (`\r\n\r\n`). Getting the socket to write the exact string structure (`HTTP/1.1 200 OK`, `Content-Type`, `Content-Length`) before dumping the raw file bytes was very insigthful.

### 2. Parameter Ordering Matters (Always Check Your Stack Traces)
During development, a swapped parameter in my path sanitizer (`rawPath` passed where `rootDirectory` was expected) taught me a valuable lesson in reading stack traces carefully. What looked like a complex directory error was just a simple argument mismatch. Debugging this sharpened my runtime diagnostics skills.

### 3. Asynchronous I/O and Socket Lifecycles
Handling `TcpClient` streams asynchronously using `async/await` showed me how modern servers handle concurrent connections without blocking. I also learned the hard way that if you don't explicitly flush your network stream and handle your using blocks correctly, the browser will hang on an `ERR_EMPTY_RESPONSE` because the connection dropped before the bytes could travel.

---

## 🚀 How to Run It

### Prerequisites
* .NET 10.0 SDK

### Setup

1. Clone the repository:
   ```bash
   git clone [https://github.com/Zaido219/static-html-server.git](https://github.com/Zaido219/static-html-server.git)
   cd static-html-server
   ```
Add your website assets (HTML, CSS, JS, images) into the StaticHtmlServer/wwwroot directory.

2. Run the application:
    ```bash
    dotnet run --project StaticHtmlServer
    ```

3. Open your browser and navigate to:
http://localhost:8080/index.html


---

## 📝 Disclaimer
It would'nt be fair if I say that I did not use AI for this build, at the same time  it would also not be fair if I say that i vibe-coded this entirely. I wrote and architected this myself, using Gemini mostly to help debug issues and clean up the code.
